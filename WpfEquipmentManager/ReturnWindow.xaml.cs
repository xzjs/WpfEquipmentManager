using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfEquipmentManager
{
    /// <summary>
    /// ReturnWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ReturnWindow : Window
    {
        public List<ReturnListItem> lrlis=new List<ReturnListItem>();
        public ReturnWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            lrlis = new List<ReturnListItem>();
            using (var context=new EMDBEntities())
            {
                List<Record> lrs = new List<Record>();
                string card = CardPhoneTextBox.Text.Trim();
                double totalMoney = 0;
                if (Combobox1.SelectedIndex==1)
                {
                    lrs = context.Records.Where(m => m.Card == "NO."+card).Where(m => m.Finish == 0).ToList();
                }else
                {
                    lrs = context.Records.Where(m => m.Phone == "Phone."+card).Where(m => m.Finish == 0).ToList();
                }
                foreach(var item in lrs)
                {
                    ReturnListItem rli = new ReturnListItem();
                    rli.id = item.Id;
                    rli.IsReturn = true;
                    rli.dateTime = Convert.ToDateTime(item.Start);
                    rli.time = Convert.ToInt32((DateTime.Now - rli.dateTime).TotalMinutes);
                    rli.name = item.Equipment.Name;
                    rli.Remain = item.LendNum;
                    rli.Num = item.LendNum;
                    rli.Equipment = item.Equipment;
                    // 计算钱数
                    rli.Money = rli.GetTotal();
                    totalMoney += rli.Money;
                    lrlis.Add(rli);
                }
                ReturnListDataGrid.DataContext = lrlis;
                TotalMoneyTextBlock.Text = totalMoney.ToString();
                if (lrlis.Count == 0)
                {
                    MessageBox.Show("没有检测到租赁记录");
                }
            }
        }

        public void UpdateMoney()
        {
            double totalMoney = 0;
            for(int i = 0; i < lrlis.Count; i++)
            {
                lrlis[i].Money = lrlis[i].GetTotal();
                if (lrlis[i].Num == 0)
                {
                    lrlis[i].IsReturn = false;
                }
                if (lrlis[i].IsReturn)
                {
                    totalMoney += lrlis[i].Money;
                }
            }
            TotalMoneyTextBlock.Text = totalMoney.ToString(); ;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // 读取list
            // 将归还数量写入
            // 将钱数写入
            // 减少没有归还的数量
            // 如果没有归还的数量为0，将finish改为1，写入归还时间
            // 设备数增加
            if (lrlis.Count == 0)
            {
                return;
            }
            using(var context=new EMDBEntities())
            {
                foreach(var item in lrlis)
                {
                    if(item.IsReturn && item.Num > 0)
                    {
                        Record r = context.Records.First(m => m.Id == item.id);
                        r.ReturnNum += item.Num;
                        r.LendNum -= item.Num;
                        r.Total += item.Money;
                        if (r.LendNum == 0)
                        {
                            r.Finish = 1;
                            r.End = DateTime.Now;
                        }
                        r.Equipment.Num += item.Num;
                        context.SaveChanges();
                    }
                }
            }
            MessageBox.Show("归还成功");
            Close();
        }

        private void ReturnListDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReturnListItem rli = (sender as DataGrid).SelectedItem as ReturnListItem;
            if (rli != null)
            {
                SelectNumWindow snw = new SelectNumWindow(rli);
                snw.rw = this;
                snw.ShowDialog();
            }
        }
    }
}
