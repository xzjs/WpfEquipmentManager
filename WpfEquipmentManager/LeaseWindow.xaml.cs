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
        private List<EquipListItem> lelis = new List<EquipListItem>();

        public LeaseWindow()
        {
            InitializeComponent();

            using(var context=new EmContext())
            {
                List<Equipment> les = context.Equipments.ToList();
                
                foreach(var item in les)
                {
                    lelis.Add(new EquipListItem { Name = item.Name, Remain = item.Num, Num = 0 });
                }
                DataGrid1.ItemsSource = lelis;
            }

            TimeTextBox.Text = DateTime.Now.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            EquipListItem eli = b.Tag as EquipListItem;
            if (b.Content.ToString() == "+")
            {
                eli.Num++;
                if (eli.Num > eli.Remain)
                {
                    eli.Num--;
                    MessageBox.Show("设备数量不足");
                }
            }
            else
            {
                if (eli.Num == 0)
                {
                    return;
                }
                eli.Num--;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<Record> lrs = new List<Record>();
            foreach(var item in lelis)
            {
                if (item.Num > 0)
                {

                }
            }
        }

        private void NumTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            EquipmentListItemBindingGroup.CommitEdit();
        }
    }
}
