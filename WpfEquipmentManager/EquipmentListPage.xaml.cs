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
    /// EquipmentListPage.xaml 的交互逻辑
    /// </summary>
    public partial class EquipmentListPage : Page
    {
        public EquipmentListPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EquipmentAddWindow eaw = new EquipmentAddWindow();
            eaw.Title = "添加设备";
            eaw.ShowDialog();
        }
    }
}
