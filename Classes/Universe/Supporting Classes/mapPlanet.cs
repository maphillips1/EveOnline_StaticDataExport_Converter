using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using EveStaticDataExportConverter.Classes.Universe.Supporting_Classes.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.Universe.Supporting_Classes
{
    internal class mapPlanet
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public long solarSystemID {  get; set; }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public long planetID { get; set; }
        [Attributes.SQLiteType("INT")]
        public int celestialIndex { get; set; }
        [Attributes.SQLIgnore()]
        public List<long> asteroidBeltIDs { get; set; }
        [Attributes.SQLIgnore()]
        public PlanetAttributes attributes { get; set; }
        [Attributes.SQLIgnore()]
        public List<long> moonIDs { get; set; }
        [Attributes.SQLiteType("INT")]
        public long orbitID { get; set; }
        [Attributes.SQLIgnore()]
        public LandmarkPosition position {  get; set; }
        [Attributes.SQLiteType("INT")]
        public double radius { get; set; }
        [Attributes.SQLIgnore()]
        public PlanetStatistics statistics { get; set; }
        [Attributes.SQLiteType("INT")]
        public int typeID { get; set; }

        [Attributes.SQLiteType("INT")]
        public string x { get; set; }
        [Attributes.SQLiteType("INT")]
        public string y { get; set; }
        [Attributes.SQLiteType("INT")]
        public string z { get; set; }
    }
}
