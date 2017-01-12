using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
                    rli.isReturn = true;
                    rli.dateTime = Convert.ToDateTime(item.Start);
                    rli.time = Convert.ToInt32((DateTime.Now - rli.dateTime).TotalMinutes);
                    rli.name = item.Equipment.Name;
                    rli.Remain = item.LendNum;
                    rli.Num = item.LendNum;
                    rli.money = 0;
                    lrlis.Add(rli);
                }
                if (lrlis.Count > 0)
                {
                    ReturnListDataGrid.DataContext = lrlis;
                }
            }
        }

        private void ReturnListDataGrid_Error(object sender, ValidationErrorEventArgs e)
        {
            //TODO:  需要写检测错误验证
        }
    }
}
