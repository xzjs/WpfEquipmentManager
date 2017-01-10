using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfEquipmentManager
{
    class Equipment
    {
        public int Id { get; set; }
        public int EquipmentClassId { get; set; }
        public virtual EquipmentClass EquipmentClass { get; set; }
    }
}
