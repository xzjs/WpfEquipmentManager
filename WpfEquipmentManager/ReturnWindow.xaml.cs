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
    /// ReturnWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ReturnWindow : Window
    {
        public ReturnWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var context=new EmContext())
            {
                List<Record> lrs;
                int phone;
                string card = CardPhoneTextBox.Text.Trim();
                if(int.TryParse(card,out phone))
                {
                    lrs = context.Records.Where(m => m.Phone == phone).ToList();
                }else
                {
                    lrs = context.Records.Where(m => m.Card == card).ToList();
                }
            }
        }
    }
}
