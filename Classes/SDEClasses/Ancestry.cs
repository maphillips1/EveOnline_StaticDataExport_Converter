using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using EveStaticDataExportConverter.Classes.Attributes;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class Ancestry : ClassWithDescription
    {
        internal Ancestry()
        {
            description = new LanguageDescription();
            shortDescription = "";
        }
        [SQLiteType("INT")]
        [SQLiteIndex()]
        [JsonProperty("_key")]
        public int ancestryID { get; set; }

        [SQLiteType("INT")]
        public int bloodlineID { get; set; }

        [SQLiteType("INT")]
        public int charisma { get; set; }
        [SQLIgnore]
        public override LanguageDescription description { get; set; }

        [SQLiteType("INT")]
        public int iconID { get; set; }

        [SQLiteType("INT")]
        public int intelligence { get; set; }

        [SQLiteType("INT")]
        public int memory { get; set; }
        [SQLIgnore]
        public LanguageDescription name { get; set; }

        [SQLiteType("INT")]
        public int perception { get; set; }

        [SQLiteType("TEXT")]
        public string shortDescription { get; set; }

        [SQLiteType("INT")]
        public int willpower { get; set; }

        public override string GetCategory()
        {
            return "Ancestry";
        }

        public override long GetKey1()
        {
            return ancestryID;
        }

        public override long GetKey2()
        {
            return 0;
        }
    }
}
