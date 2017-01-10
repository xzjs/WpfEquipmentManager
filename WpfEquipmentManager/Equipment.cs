using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfEquipmentManager
{
    class Equipment
    {
        public long Id { get; set; }
        public long EquipmentClassId { get; set; }
        public virtual EquipmentClass EquipmentClass { get; set; }
    }
}
