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
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page
    {
        public MainWindow mw;
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LeaseWindow lw = new LeaseWindow();
                lw.ShowDialog();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ReturnWindow rw = new ReturnWindow();
            rw.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            mw.PageFrame.Content = new EquipmentListPage();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            mw.PageFrame.Content = new AccountPage();
        }
    }
}
