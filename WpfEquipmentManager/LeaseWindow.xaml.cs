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
    /// LeaseWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LeaseWindow : Window
    {
        private List<MyListItem> lelis = new List<MyListItem>();

        public LeaseWindow()
        {
            InitializeComponent();
            try
            {
                using (var context = new EMDBEntities())
                {
                    List<Equipment> les = context.Equipments.ToList();

                    foreach (var item in les)
                    {
                        lelis.Add(new MyListItem { Equipment = item, Num = 0, Remain = item.Num });
                    }
                    DataGrid1.ItemsSource = lelis;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }

            TimeTextBox.Text = DateTime.Now.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (CardTextBox.Text.Trim() == "" && PhoneTextBox.Text.Trim() == "") {
                MessageBox.Show("一卡通和手机号不可都为空");
                return;
            }
            int i = 0;
            if(int.TryParse(NameTextBox.Text, out i))
            {
                MessageBox.Show("租借人姓名不可为纯数字");
                return;
            }
            try
            {
                List<Record> lrs = new List<Record>();
                foreach (var item in lelis)
                {
                    if (item.Num > 0)
                    {
                        lrs.Add(new Record
                        {
                            Name = NameTextBox.Text,
                            Card = "NO."+CardTextBox.Text,
                            Phone = "Phone." + PhoneTextBox.Text,
                            Start = DateTime.Now,
                            EquipmentId = item.Equipment.Id,
                            LendNum = item.Num,
                            ReturnNum = 0,
                            Finish = 0,
                            Total = 0
                        });
                    }
                }
                if (lrs.Count > 0)
                {
                    using (var context = new EMDBEntities())
                    {
                        context.Records.AddRange(lrs);
                        context.SaveChanges();
                        foreach(var item in lelis)
                        {
                            Equipment eq = context.Equipments.First(m => m.Id == item.Equipment.Id);
                            eq.Num -= item.Num;
                            context.SaveChanges();
                        }
                    }
                    MessageBox.Show("租赁成功");
                    Close();
                }
                else
                {
                    throw new Exception("请选择租赁设备");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void DataGrid1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MyListItem mli = DataGrid1.SelectedItem as MyListItem;
            if (mli != null)
            {
                SelectNumWindow snw = new SelectNumWindow(mli);
                snw.ShowDialog();
            }
            
        }
    }
}
