using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfEquipmentManager
{
    public class Equipment
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Num { get; set; }
        public double Price { get; set; }
        public long Type { get; set; }
        public long Detail { get; set; }
        public string Remark { get; set; }
        public virtual List<Record> Records { get; set; }
    }

    public class Record
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public string Card { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public long EquipmentId { get; set; }
        public int LendNum { get; set; }
        public int Finish { get; set; }
        public double Total { get; set; }
        public int ReturnNum { get; set; }

        public virtual Equipment Equipment { get; set; }
    }

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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }

    public class EquipListItem : MyListItem
    {
        public Equipment Equipment { get; set; }
    }

    public class ReturnListItem : MyListItem
    {
        private bool isReturn;
        public DateTime dateTime { get; set; }
        public int time { get; set; }
        public string name { get; set; }
        private double money;
        public Equipment Equipment { get; set; }

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
            long type = Equipment.Type;
            long detail = Equipment.Detail;
            double price = Equipment.Price;
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
}
