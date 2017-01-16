using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// SetupPage.xaml 的交互逻辑
    /// </summary>
    public partial class SetupPage : Page
    {
        public SetupPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new EMDBEntities())
            {
                Key k = db.Keys.First();
                if (k.Activation == "请输入激活码")
                {
                    SerialTextBox.Text = k.Serial;
                }
                else
                {
                    isActiveStackPanel.Visibility = Visibility.Visible;
                    notActiveStackPanel.Visibility = Visibility.Hidden;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            string str = SerialTextBox.Text + "xzjs";
            string result = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(str)), 4, 8);
            result= result.Replace("-", "").ToLower();
            if (result == ActiveTextBox.Text)
            {
                using(var db=new EMDBEntities())
                {
                    Key k = db.Keys.First();
                    k.Activation = ActiveTextBox.Text;
                    db.SaveChanges();
                    MessageBox.Show("激活成功");
                }
            }
            else
            {
                MessageBox.Show("激活码错误");
            }
        }
    }
}
