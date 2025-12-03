using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using EveStaticDataExportConverter.Classes.SDEClasses;
using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.Universe
{
    internal class mapRegion
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public long regionID {  get; set; }
        [Attributes.SQLIgnore()]
        public List<long> constellationIDs { get;set; }
        [Attributes.SQLIgnore()]
        public LanguageDescription description { get; set; }
        [Attributes.SQLiteType("INT")]
        public long factionID { get; set; }
        [Attributes.SQLIgnore()]
        public LanguageDescription name { get; set; }
        [Attributes.SQLiteType("INT")]
        public int nebulaID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public int wormholeClassID { get; set; }
        [Attributes.SQLIgnore()]
        public LandmarkPosition position { get; set; }
        [Attributes.SQLiteType("INT")]
        public string x { get; set; }
        [Attributes.SQLiteType("INT")]
        public string y { get; set; }
        [Attributes.SQLiteType("INT")]
        public string z { get; set; }
    }
}
