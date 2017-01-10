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
        private int ecid;
        private EquipmentClass ec;
        public EquipmentAddWindow(int id=0)
        {
            InitializeComponent();
            this.ecid = id;
            if (id == 0)
            {
                Title = "添加设备";
                ec = new EquipmentClass();
            }else
            {
                Title = "编辑设备";
                using(var context=new EmContext())
                {
                    var ecs = from a in context.EquipmentClasses
                             where a.Id == ecid
                             select a;
                    ec = ecs.First();
                }
            }
            EquipmentClassDetail.DataContext = ec;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ecid == 0)
                {
                    using (var context = new EmContext())
                    {
                        List<Equipment> lq = new List<Equipment>();
                        for (int i = 0; i < ec.Num; i++)
                        {
                            lq.Add(new Equipment());
                        }
                        ec.Equipments = lq;
                        context.EquipmentClasses.Add(ec);
                        context.SaveChanges();
                        MessageBox.Show("添加成功");
                        Close();
                    }
                }
                else
                {
                    using(var context=new EmContext())
                    {

                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }     
    }
}
