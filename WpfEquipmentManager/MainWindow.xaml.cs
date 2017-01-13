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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PageFrame.Content = new MainPage();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new EquipmentListPage();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new AccountPage();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            LeaseWindow lw = new LeaseWindow();
            lw.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ReturnWindow rw = new ReturnWindow();
            rw.ShowDialog();
        }
    }
}
