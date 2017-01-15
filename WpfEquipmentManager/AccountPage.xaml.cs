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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfEquipmentManager
{
    /// <summary>
    /// AccountPage.xaml 的交互逻辑
    /// </summary>
    public partial class AccountPage : Page
    {
        public AccountPage()
        {
            InitializeComponent();

            using(var context=new EMDBEntities())
            {
                List<Record> lr = context.Records.Include("Equipment").OrderByDescending(m => m.Start).ToList();
                RecordListDataGrid.ItemsSource = lr;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Set_Button(ref AllButton);
            Set_Button(ref FinishButton);
            Set_Button(ref UnFinishButton);
            Button b = sender as Button;
            Set_Button(ref b, "#FF3399FF", "White"); 
            DateTime? startDate = StartDate.SelectedDate;
            DateTime? endDate = EndDate.SelectedDate;
            if (endDate < startDate)
            {
                MessageBox.Show("结束日期必须大于开始日期");
                return;
            }
            using(var context=new EMDBEntities())
            {
                var records = context.Records.Include("Equipment").Where(m => m.Start > startDate && m.Start < endDate);
                if (b.Content.ToString() != "全部订单")
                {
                    int flag = 0;
                    if (b.Content.ToString() == "已完成订单")
                    {
                        flag = 1;
                    }
                    records = records.Where(m => m.Finish == flag);
                }
                List<Record> lr = records.OrderByDescending(m => m.Start).ToList();
                RecordListDataGrid.ItemsSource = lr;
                if (lr.Count == 0)
                {
                    MessageBox.Show("没有相关记录");
                }
            }
        }

        private void Set_Button(ref Button b, string bgc="#FFDDDDDD",string fgc="Black")
        {
            Color c = (Color)ColorConverter.ConvertFromString(bgc);
            b.Background = new SolidColorBrush(c);
            c = (Color)ColorConverter.ConvertFromString(fgc);
            b.Foreground = new SolidColorBrush(c);
        }
    }
}
