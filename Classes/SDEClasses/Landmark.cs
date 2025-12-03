
using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class Landmark
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public int landmarkID {get;set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription description { get;set;}

        [Attributes.SQLiteType("INT")]
        public long locationID { get;set;}

        [Attributes.SQLIgnore()]
        public LanguageDescription name { get;set;}

        [Attributes.SQLIgnore()]
        public LandmarkPosition position { get;set; }

        [Attributes.SQLiteType("TEXT")]
        public string x { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string y { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string z { get; set; }
    }
}
