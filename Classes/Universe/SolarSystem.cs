using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveStaticDataExportConverter.Classes.Universe.Supporting_Classes;
using EveStaticDataExportConverter.Classes.Universe.Supporting_Classes.Converters;
using EveStaticDataExportConverter.Classes.Universe.SupportingClasses;

namespace EveStaticDataExportConverter.Classes.Universe
{
    internal class SolarSystem
    {
        public SolarSystem()
        {
            solarSystemName = "";
            securityClass = "";
        }

        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public long regionID {  get; set; }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public long constellationId { get; set; }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public long solarSystemID { get; set; }
        [Attributes.SQLiteType("INT")]
        public long factionID { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string solarSystemName { get; set; }
        [Attributes.SQLiteType("INT")]
        public bool border {  get; set; }
        [Attributes.SQLIgnore()]
        public List<double> center {  get; set; }
        [Attributes.SQLiteType("INT")]
        public bool corridor { get; set; }
        [Attributes.SQLiteType("INT")]
        public bool fringe {  get; set; }
        [Attributes.SQLiteType("INT")]
        public bool hub { get; set; }
        [Attributes.SQLiteType("INT")]
        public bool international { get; set; }
        [Attributes.SQLiteType("INT")]
        public decimal luminosity { get; set; }
        [Attributes.SQLIgnore()]
        public List<double> max {  get; set; }
        [Attributes.SQLIgnore()]
        public List<double> min { get; set; }
        [Attributes.SQLiteType("INT")]
        public double radius { get; set; }
        [Attributes.SQLiteType("INT")]
        public bool regional { get; set; }
        [Attributes.SQLiteType("INT")]
        public double security { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string securityClass { get; set; }
        [Attributes.SQLiteType("INT")]
        public int solarSystemNameID { get; set; }
        [Attributes.SQLiteType("INT")]
        public int sunTypeID {  get; set; }

        [Attributes.SQLIgnore() ]
        [Newtonsoft.Json.JsonConverter(typeof(SolarSystemPlanetConverter))]
        public List<SolarSystemPlanet> planets { get; set; }

        [Attributes.SQLIgnore()]
        public SolarSystemStar star { get; set; }

        [Attributes.SQLIgnore() ]
        [Newtonsoft.Json.JsonConverter(typeof(SolarSystemStargateConverter))]
        public List<SolarSystemStargate> stargates { get; set; }

        [Attributes.SQLiteType("INT")]
        public double centerX { get; set; }
        [Attributes.SQLiteType("INT")]
        public double centerY { get; set; }
        [Attributes.SQLiteType("INT")]
        public double centerZ { get; set; }

        [Attributes.SQLiteType("INT")]
        public double maxX { get; set; }
        [Attributes.SQLiteType("INT")]
        public double maxY { get; set; }
        [Attributes.SQLiteType("INT")]
        public double maxZ { get; set; }

        [Attributes.SQLiteType("INT")]
        public double minX { get; set; }
        [Attributes.SQLiteType("INT")]
        public double minY { get; set; }
        [Attributes.SQLiteType("INT")]
        public double minZ { get; set; }
    }
}
