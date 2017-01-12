using System;
using System.Collections.Generic;
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

    public class MyListItem
    {
        public int Remain { get; set; }
        public int Num { get; set; }
    }

    public class EquipListItem:MyListItem
    {
        public Equipment Equipment { get; set; }
    }

    public class ReturnListItem:MyListItem
    {
        public bool isReturn { get; set; }
        public DateTime dateTime { get; set; }
        public int time { get; set; }
        public string name { get; set; }
        public double money { get; set; }
    }
}
