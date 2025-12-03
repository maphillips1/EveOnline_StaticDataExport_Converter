using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class PlanetResource
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public long planetID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public int amount_per_cycle { get; set; }
        [Attributes.SQLiteType("INT")]
        public int cycle_period { get; set; }
        [Attributes.SQLiteType("INT")]
        public int power {  get; set; }
        [Attributes.SQLiteType("INT")]
        public int secured_capacity { get; set; }
        [Attributes.SQLiteType("INT")]
        public int unsecured_capacity { get; set; }
        [Attributes.SQLiteType("INT")]
        public int reagent_type_id { get; set; }
        [Attributes.SQLiteType("INT")]
        public int workforce {  get; set; }

        [Attributes.SQLIgnore()]
        public PlanetResourceReagent reagent { get; set; }
    }
}
