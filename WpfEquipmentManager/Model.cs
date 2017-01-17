using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfEquipmentManager
{
    /// <summary>
    /// 租赁设备单项
    /// </summary>
    public class MyListItem:INotifyPropertyChanged
    {
        public int Remain { get; set; }

        public int Num
        {
            get
            {
                return num;
            }

            set
            {
                if (num == value)
                {
                    return;
                }
                num = value;
                Notify("Num");
            }
        }

        protected int num;
        public Equipment Equipment { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }

    /// <summary>
    /// 归还设备单项
    /// </summary>
    public class ReturnListItem : MyListItem
    {
        public long id { get; set; }
        private bool isReturn;
        public DateTime dateTime { get; set; }
        public int time { get; set; }
        public string name { get; set; }
        private double money;

        public double Money
        {
            get
            {
                return money;
            }

            set
            {
                if (money == value)
                {
                    return;
                }
                money = value;
                Notify("Money");
            }
        }

        public bool IsReturn
        {
            get
            {
                return isReturn;
            }

            set
            {
                if (isReturn == value)
                {
                    return;
                }
                isReturn = value;
                Notify("IsReturn");
            }
        }

        /// <summary>
        /// 计算钱数
        /// </summary>
        public double GetTotal()
        {
            long type =(long)Equipment.Type;
            long detail = (long)Equipment.Detail;
            double price = (double)Equipment.Price;
            int t = 0;
            if (detail == 0)
            {
                if (type == 0)
                {
                    t = Convert.ToInt32(Math.Ceiling(time / 60.0));
                }
                else
                {
                    t = Convert.ToInt32(Math.Ceiling(time / 30.0));
                }
            }
            else
            {
                t = time;
                if (type == 0)
                {
                    price /= 60.0;
                }
                else
                {
                    price /= 30.0;
                }
            }
            double total = price * t * Num;
            total = Math.Round(total, 2);
            return total;
        }
    }

    /// <summary>
    /// 记录列表单项
    /// </summary>
    public class RecordListItem
    {
        public long Id { get; set; }
        public String Start { get; set; }
        public String End { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Card { get; set; }
        public string EquipmentName { get; set; }
        public string Total { get; set; }
        public int LendNum { get; set; }
        public int ReturnNum { get; set; }
    }

    /// <summary>
    /// 计价规则转换
    /// </summary>
    public class PriceConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string type_num = values[1].ToString();
            string detail_num = values[2].ToString();
            string type = type_num == "0" ? "小时" : "半小时";
            string detail = type_num == "0" ? "向上取整" : "精确到分钟";
            return values[0]+"元/"+type+"，"+detail;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return ((string)value).Split(' ');
        }
    }

    /// <summary>
    /// 显示金额
    /// </summary>
    public class TotalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double d = (double)value;
            if (d == 0)
            {
                return "尚未归还";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    /// <summary>
    /// 数量验证
    /// </summary>
    public class NumRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                int num = Convert.ToInt32(value);
                if (num > 0)
                {
                    return new ValidationResult(true, null);
                }
                else
                {
                    return new ValidationResult(false, "请输入大于0的数");
                }
            }
            catch(Exception ex)
            {
                return new ValidationResult(false, "请输入数字");
            }
        }
    }
    /// <summary>
    /// 金额验证
    /// </summary>
    public class DecimalRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                double num = Convert.ToDouble(value);
                if (num > 0)
                {
                    return new ValidationResult(true, null);
                }
                else
                {
                    return new ValidationResult(false, "请输入大于0的数");
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "请输入数字");
            }
        }
    }
}
