using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.Universe.Supporting_Classes
{
    internal class mapMoon
    {
        [Attributes.SQLiteType("INT")]
        public long solarSystemID { get; set; }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("orbitID")]
        public long planetID {  get; set; }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public long moonID { get; set; }
        [Attributes.SQLiteType("INT")]
        public int celestialIndex { get; set; }
        [Attributes.SQLiteType("INT")]
        public int orbitIndex { get; set; }
        [Attributes.SQLIgnore()]
        public MoonAttributes attributes { get; set; }
        [Attributes.SQLIgnore()]
        public LandmarkPosition position { get; set; }
        [Attributes.SQLiteType("INT")]
        public double radius { get; set; }
        [Attributes.SQLiteType("INT")]
        public int typeID { get; set; }

        [Attributes.SQLiteType("INT")]
        public string x { get; set; }
        [Attributes.SQLiteType("INT")]
        public string y { get; set; }
        [Attributes.SQLiteType("INT")]
        public string z { get; set; }
        [Attributes.SQLIgnore() ]
        public MoonStatistics statistics { get; set; }
    }
}
