using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfEquipmentManager
{
    class EquipmentClass
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long Num { get; set; }
        public double Price { get; set; }
        public long Type { get; set; }
        public long Detail { get; set; }
        public string Remark { get; set; }
        
        public virtual ICollection<Equipment> Equipments { get; set; }
        public EquipmentClass()
        {
            Equipments = new List<Equipment>(); 
        }
    }
}
