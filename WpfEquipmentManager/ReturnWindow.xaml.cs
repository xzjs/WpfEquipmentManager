using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfEquipmentManager
{
    /// <summary>
    /// ReturnWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ReturnWindow : Window
    {
        public List<ReturnListItem> lrlis = new List<ReturnListItem>();
        public ReturnWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var context=new EmContext())
            {
                List<Record> lrs;
                int phone;
                string card = CardPhoneTextBox.Text.Trim();
                double totalMoney = 0;
                if(int.TryParse(card,out phone))
                {
                    lrs = context.Records.Where(m => m.Phone == phone).Where(m=>m.Finish==0).ToList();
                }else
                {
                    lrs = context.Records.Where(m => m.Card == card).Where(m => m.Finish == 0).ToList();
                }
                foreach(var item in lrs)
                {
                    ReturnListItem rli = new ReturnListItem();
                    rli.IsReturn = true;
                    rli.dateTime = Convert.ToDateTime(item.Start);
                    rli.time = Convert.ToInt32((DateTime.Now - rli.dateTime).TotalMinutes);
                    rli.name = item.Equipment.Name;
                    rli.Remain = item.LendNum;
                    rli.Num = item.LendNum;
                    rli.Equipment = item.Equipment;
                    //TODO 计算钱数
                    rli.Money = rli.GetTotal();
                    totalMoney += rli.Money;
                    lrlis.Add(rli);
                }
                if (lrlis.Count > 0)
                {
                    ReturnListDataGrid.DataContext = lrlis;
                    TotalMoneyTextBlock.Text = totalMoney.ToString();
                }
            }
        }

        private void ReturnListDataGrid_Error(object sender, ValidationErrorEventArgs e)
        {
            //TODO:  需要写检测错误验证
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //Timer t = new Timer(500);
            //t.Elapsed += UpdateMoney;
            //t.AutoReset = false;
            //t.Enabled = true;

            DispatcherTimer readDataTimer = new DispatcherTimer();
            readDataTimer.Tick += new EventHandler(UpdateMoney);
            readDataTimer.Interval = new TimeSpan(0,0,1);
            readDataTimer.Start();
        }

        private void UpdateMoney(object source,EventArgs e)
        {
            double totalMoney = 0;
            for(int i = 0; i < lrlis.Count; i++)
            {
                lrlis[i].Money = lrlis[i].GetTotal();
                if (lrlis[i].IsReturn)
                {
                    totalMoney += lrlis[i].Money;
                }
            }
            TotalMoneyTextBlock.Text = totalMoney.ToString(); ;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO 读取list
            //TODO 将归还数量写入
            //TODO 将钱数写入
            //TODO 减少没有归还的数量
            //TODO 如果没有归还的数量为0，将finish改为1，写入归还时间
        }
    }
}
