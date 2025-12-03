using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.Universe
{
    internal class mapConstellation
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public long regionID {  get; set; }

        [Attributes.SQLiteIndex()]
        [Attributes.SQLiteType("INT")]
        [Newtonsoft.Json.JsonProperty("_key")]
        public long constellationID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public long factionID { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription name { get; set; }
        [Attributes.SQLIgnore()]
        public LandmarkPosition position { get; set; }
        [Attributes.SQLIgnore()]
        public List<long> solarSystemIDs { get; set; }
        [Attributes.SQLiteType("INT")]
        public string x { get; set; }
        [Attributes.SQLiteType("INT")]
        public string y { get; set; }
        [Attributes.SQLiteType("INT")]
        public string z { get; set; }
        [Attributes.SQLiteType("INT")]
        public int wormholeClassID { get; set; }
    }
}
