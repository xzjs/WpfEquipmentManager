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
    /// EquipmentAddWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EquipmentAddWindow : Window
    {
        public EquipmentAddWindow()
        {
            InitializeComponent(); 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new EmContext())
                {
                    EquipmentClass ec = new EquipmentClass
                    {
                        Name = Name.Text,
                        Num = Convert.ToInt32(Num.Text),
                        Price = Convert.ToDouble(Price.Text),
                        Type = Type.Text,
                        Detail = Detail.Text,
                        Remark = Remark.Text
                    };
                    List<Equipment> lq = new List<Equipment>();
                    for(int i = 0; i < ec.Num; i++)
                    {
                        lq.Add(new Equipment());
                    }
                    ec.Equipments = lq;
                    context.EquipmentClasses.Add(ec);
                    context.SaveChanges();
                    MessageBox.Show("添加成功");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }     
    }
}
