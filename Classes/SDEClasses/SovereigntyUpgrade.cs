using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class SovereigntyUpgrade
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public int sovereigntyUpgradeID {  get; set; }

        [Attributes.SQLiteType("INT")]
        public int fuel_hourly_upkeep { get; set; }

        [Attributes.SQLiteType("INT")]
        public int fuel_startup_cost { get; set; }

        [Attributes.SQLiteType("INT")]
        public int fuel_type_id { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string mutually_exclusive_group { get; set; }

        [Attributes.SQLiteType("INT")]
        public int power_allocation {  get; set; }

        [Attributes.SQLiteType("INT")]
        public int workforce_allocation { get; set; }

        [Attributes.SQLIgnore()]
        public SovereigntyUpgradeFuel fuel { get; set; }

    }
}
