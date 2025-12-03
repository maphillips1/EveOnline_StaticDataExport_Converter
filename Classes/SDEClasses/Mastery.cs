using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class Mastery
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public int masteryId {  get; set; }

        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int level { get; set; }

        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int value { get; set; }
        [Attributes.SQLIgnore()]
        [Newtonsoft.Json.JsonProperty("_value")]
        public List<MasteryLevel> levels { get; set; }

    }
}
