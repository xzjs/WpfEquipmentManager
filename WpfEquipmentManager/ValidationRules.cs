using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfEquipmentManager
{
    public class EquipmentNumRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                BindingGroup bindingGroup = (BindingGroup)value;
                MyListItem eli = (MyListItem)bindingGroup.Items[0];
                int reamin = Convert.ToInt32(bindingGroup.GetValue(eli, "Remain"));
                int num = Convert.ToInt32(bindingGroup.GetValue(eli, "Num"));
                if (num < 0)
                {
                    return new ValidationResult(false, "输入的数字不合法");
                }
                if (num > reamin)
                {
                    return new ValidationResult(false, "租赁数量超过剩余数量");
                }
                else
                {
                    return new ValidationResult(true, null);
                }
            }
            catch(Exception ex)
            {
                return new ValidationResult(false, ex.Message);
            }
        }
    }

    public class ReturnListNumRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            BindingGroup bindingGroup = (BindingGroup)value;
            ReturnListItem eli = (ReturnListItem)bindingGroup.Items[0];
            int reamin = eli.Remain;
            int num = eli.Num;
            if (num < 0)
            {
                return new ValidationResult(false, "输入的数字不合法");
            }
            if (num > reamin)
            {
                return new ValidationResult(false, "归还数量超过租赁数量");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
