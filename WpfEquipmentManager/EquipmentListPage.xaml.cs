using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfEquipmentManager
{
    /// <summary>
    /// EquipmentListPage.xaml 的交互逻辑
    /// </summary>
    public partial class EquipmentListPage : Page
    {
        public EquipmentListPage()
        {
            InitializeComponent();

            using (var context = new EmContext())
            {
                var ecs = from a in context.EquipmentClasses
                          select a;
                if (ecs!=null)
                {
                    DataGrid1.ItemsSource = ecs.ToList();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EquipmentAddWindow eaw = new EquipmentAddWindow();
            eaw.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32((sender as Button).Tag);
            EquipmentAddWindow eaw = new EquipmentAddWindow(id);
            eaw.ShowDialog();
        }
    }
}
