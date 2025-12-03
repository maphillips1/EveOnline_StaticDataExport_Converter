using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class Bloodlines
    {
        public Bloodlines()
        {
            description = new LanguageDescription();
            name = new LanguageDescription();
        }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public int bloodlinesID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int charisma { get; set; }

        [Attributes.SQLiteType("INT")]
        public int corporationID { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription description { get; set; }

        [Attributes.SQLiteType("INT")]
        public int iconID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int intelligence { get; set; }

        [Attributes.SQLiteType("INT")]
        public int memory { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription name { get; set; }

        [Attributes.SQLiteType("INT")]
        public int perception { get; set; }

        [Attributes.SQLiteType("INT")]
        public int raceID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int willpower { get; set; }
    }
}
