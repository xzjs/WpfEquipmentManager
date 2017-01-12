using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfEquipmentManager
{
    class EmContext:DbContext
    {
        public DbSet<Equipment> Equipments { get; set; }
    }
}
