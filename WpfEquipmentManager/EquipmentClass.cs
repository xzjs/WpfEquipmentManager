using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfEquipmentManager
{
    class EquipmentClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Num { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }
        public string Detail { get; set; }
        public string Remark { get; set; }
        public EquipmentClass()
        {
            Equipments = new List<Equipment>();
        }
        public virtual ICollection<Equipment> Equipments { get; set; }
    }
}
