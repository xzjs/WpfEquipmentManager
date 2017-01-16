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
using System.Data.Entity;
using System.Collections.ObjectModel;

namespace WpfEquipmentManager
{
    /// <summary>
    /// EquipmentAddWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EquipmentAddWindow : Window
    {
        private Equipment ec;
        private int num;
        public ObservableCollection<Equipment> ocec { get; set; }

        public EquipmentAddWindow(Equipment _ec)
        {
            InitializeComponent();
            ec = _ec;
            num = (int)_ec.Num;
            EquipmentClassDetail.DataContext = ec;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<TextBox> ltb = new List<TextBox>
            {
                Num,Price
            };
            foreach(var item in ltb)
            {
                if (Validation.GetHasError(item))
                {
                    List<ValidationError> lv = Validation.GetErrors(item).ToList();
                    foreach (var v in lv)
                    {
                        MessageBox.Show(v.ErrorContent.ToString());
                    }
                    return;
                }
            }
            try
            {
                using (var context = new EMDBEntities())
                {
                    if (ec.Id == 0)
                    {
                        context.Equipments.Add(ec);
                        context.SaveChanges();
                        ocec.Add(ec);
                    }
                    else
                    {
                        ec.Remark += "。";
                        context.Entry(ec).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
                MessageBox.Show("操作成功");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }     
    }
}
