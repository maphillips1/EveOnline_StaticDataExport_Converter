using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.Universe.Supporting_Classes
{
    internal class mapAsteroidBelt
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("orbitID")]
        public long planetID { get; set; }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public long asteroidBeltID { get; set; }
        [Attributes.SQLIgnore()]
        public LandmarkPosition position { get; set; }
        [Attributes.SQLIgnore()]
        public AsteroidBetlStatistics statistics { get; set; }
        [Attributes.SQLiteType("INT")]
        public int typeID { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string positionX { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string positionY { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string positionZ { get; set; }
        [Attributes.SQLiteType("INT")]
        public int celestialIndex { get; set; }
        [Attributes.SQLiteType("INT")]
        public int orbitIndex { get; set; }
        [Attributes.SQLiteType("INT")]
        public long solarSystemID { get; set; }
        [Attributes.SQLiteType("INT")]
        public double radius {  get; set; }
    }
}
