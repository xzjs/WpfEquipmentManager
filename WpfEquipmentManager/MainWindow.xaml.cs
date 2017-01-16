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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainPage mp = new MainPage();
            mp.mw = this;
            PageFrame.Content = mp;
           
            
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

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            TestWindow tw = new TestWindow();
            tw.ShowDialog();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            MainPage mp = new MainPage();
            mp.mw = this;
            PageFrame.Content = mp;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new EMDBEntities())
            {
                Key k = db.Keys.FirstOrDefault();
                MD5 md5 = new MD5CryptoServiceProvider();
                if (k == null)
                {
                    MessageBox.Show("当前版本未激活，可免费试用15天");
                    Key key = new Key();

                    string str = Guid.NewGuid().ToString();
                    string result = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(str)), 4, 8);
                    result= result.Replace("-", "").ToLower();
                    key.Serial = result;
                    key.Activation = "请输入激活码";
                    key.Time = DateTime.Today;
                    db.Keys.Add(key);
                    db.SaveChanges();
                }
                else
                {
                    if (k.Activation == "请输入激活码")
                    {
                        TimeSpan ts = DateTime.Today - k.Time;
                        if (ts.Days > 15)
                        {
                            MessageBox.Show("试用期已到");
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("还可试用" + (15 - ts.Days) + "天");
                        }
                    }
                    else
                    {
                        string result = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(k.Serial+"xzjs")), 4, 8);
                        result = result.Replace("-", "").ToLower();
                        if (result != k.Activation)
                        {
                            MessageBox.Show("激活码错误");
                            Close();
                        }
                    }
                }
            }
        }
    }
}
