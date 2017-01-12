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
            BindingGroup bindingGroup = (BindingGroup)value;
            EquipListItem eli = (EquipListItem)bindingGroup.Items[0];
            int reamin = Convert.ToInt32(bindingGroup.GetValue(eli, "Remain"));
            int num = Convert.ToInt32(bindingGroup.GetValue(eli, "Num"));
            if (num > reamin)
            {
                return new ValidationResult(false, "租赁数量超过剩余数量");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
