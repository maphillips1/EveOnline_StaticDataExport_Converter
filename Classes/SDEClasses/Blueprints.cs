using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class Blueprints
    {
        [Attributes.SQLIgnore()]
        public BlueprintActivities activities { get; set; }

        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public int blueprintTypeID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int maxProductionLimit { get; set; }
    }
}
