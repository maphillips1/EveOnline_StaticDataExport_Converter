using EveStaticDataExportConverter.Classes.Universe.Supporting_Classes.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.Universe.Supporting_Classes
{
    internal class SolarSystemPlanet
    {
        [Attributes.SQLiteType("INT")]
        public long solarSystemID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public long planetID { get; set; }
        [Attributes.SQLIgnore()]
        [Newtonsoft.Json.JsonConverter(typeof(PlanetAsteroidBeltConverter))]
        public List<PlanetAsteroidBelt> asteroidBelts { get; set; }
        [Attributes.SQLiteType("INT")]
        public int celestialIndex { get; set; }
        [Attributes.SQLIgnore()]
        public PlanetAttributes planetAttributes { get; set; }
        [Attributes.SQLIgnore()]
        [Newtonsoft.Json.JsonConverter(typeof(PlanetMoonConverter))]
        public List<PlanetMoon> moons { get; set; }
        [Attributes.SQLIgnore()]
        public List<double> position {  get; set; }
        [Attributes.SQLiteType("INT")]
        public double radius { get; set; }
        [Attributes.SQLIgnore()]
        public PlanetStatistics statistics { get; set; }
        [Attributes.SQLiteType("INT")]
        public int typeID { get; set; }

        [Attributes.SQLiteType("INT")]
        public double positionX { get; set; }
        [Attributes.SQLiteType("INT")]
        public double positionY { get; set; }
        [Attributes.SQLiteType("INT")]
        public double positionZ { get; set; }
    }
}
