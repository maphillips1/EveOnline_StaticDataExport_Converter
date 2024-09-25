using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
{
    internal class PlanetResource
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public long planetID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public int cycle_minutes { get; set; }
        [Attributes.SQLiteType("INT")]
        public int harvest_silo_max { get; set; }
        [Attributes.SQLiteType("INT")]
        public int maturation_cycle_minutes { get; set; }
        [Attributes.SQLiteType("INT")]
        public int maturation_percent {  get; set; }
        [Attributes.SQLiteType("INT")]
        public decimal mature_silo_max { get; set; }
        [Attributes.SQLiteType("INT")]
        public int power {  get; set; }
        [Attributes.SQLiteType("INT")]
        public int reagent_harvest_amount { get; set; }
        [Attributes.SQLiteType("INT")]
        public int reagent_type_id { get; set; }
        [Attributes.SQLiteType("INT")]
        public int workforce {  get; set; }
    }
}
