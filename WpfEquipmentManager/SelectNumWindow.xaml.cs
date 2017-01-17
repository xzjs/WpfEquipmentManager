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
    /// SelectNumWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SelectNumWindow : Window
    {
        public ReturnWindow rw;
        public SelectNumWindow(MyListItem mli)
        {
            InitializeComponent();

            MylistItemStackPanel.DataContext = mli;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (rw!=null)
            {
                rw.UpdateMoney();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(MylistItemStackPanel))
            {
                List<ValidationError> lv = Validation.GetErrors(MylistItemStackPanel).ToList();
                foreach (var v in lv)
                {
                    MessageBox.Show(v.ErrorContent.ToString());
                }
                return;
            }
            Close();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SelectNumBindingGroup.CommitEdit();
        }
    }
}
