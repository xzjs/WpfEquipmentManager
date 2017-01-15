using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
        public ObservableCollection<Equipment> ecs;

        public EquipmentListPage()
        {
            InitializeComponent();

            using (var context = new EMDBEntities())
            {
                ecs = new ObservableCollection<Equipment>(context.Equipments.ToList());

                DataGrid1.ItemsSource = ecs;

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Equipment new_ec = new Equipment();
            EquipmentAddWindow eaw = new EquipmentAddWindow(new_ec);
            eaw.ocec = ecs;
            eaw.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Equipment ec = (sender as Button).Tag as Equipment;
            EquipmentAddWindow eaw = new EquipmentAddWindow(ec);
            eaw.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Equipment ec = (sender as Button).Tag as Equipment;
            MessageBoxResult mbr = MessageBox.Show("是否删除" + ec.Name,"删除",MessageBoxButton.OKCancel,MessageBoxImage.Warning);
            if (mbr == MessageBoxResult.OK)
            {
                using (var context = new EMDBEntities())
                {
                    context.Entry(ec).State = EntityState.Deleted;
                    context.SaveChanges();
                    ecs.Remove(ec);

                }
            }
        }
    }
}
