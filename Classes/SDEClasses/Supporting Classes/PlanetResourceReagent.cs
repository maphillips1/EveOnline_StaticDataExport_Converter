using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes
{
    internal class PlanetResourceReagent
    {
        public int amount_per_cycle {  get; set; }
        public int cycle_period { get; set; }
        public int secured_capacity { get; set; }
        public int type_id { get; set; }
        public int unsecured_capacity { get; set; }
    }
}
