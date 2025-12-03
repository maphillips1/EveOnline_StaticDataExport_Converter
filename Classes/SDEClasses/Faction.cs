using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class Faction
    {
        [Attributes.SQLiteIndex()]
        [Attributes.SQLiteType("INT")]
        [Newtonsoft.Json.JsonProperty("_key")]
        public long factionID { get; set; }

        [Attributes.SQLiteType("INT")]
        public long corporationID { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription description { get; set; }

        [Attributes.SQLiteType("INT")]
        public int iconID { get; set; }

        [Attributes.SQLIgnore()]
        public List<int> memberRaces { get; set; }

        [Attributes.SQLiteType("INT")]
        public long militiaCorporationID { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription name { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription shortDescription { get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal sizeFactor { get; set; }

        [Attributes.SQLiteType("INT")]
        public long solarSystemID { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool uniqueName { get; set; }

    }
}
