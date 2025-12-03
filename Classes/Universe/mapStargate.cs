using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.Universe
{
    internal class mapStargate
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public long stargateID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public long solarSystemID { get; set; }
        [Attributes.SQLiteType("INT")]
        public int typeID { get; set; }
        [Attributes.SQLIgnore()]
        public LandmarkPosition position { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string x {  get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string y { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string z { get; set; }
        [Attributes.SQLIgnore() ]
        public StargateDestination destination { get; set; }
    }
}
