using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using EveStaticDataExportConverter.Classes.Universe.Supporting_Classes;
using EveStaticDataExportConverter.Classes.Universe.Supporting_Classes.Converters;
using EveStaticDataExportConverter.Classes.Universe.SupportingClasses;

namespace EveStaticDataExportConverter.Classes.Universe
{
    internal class mapSolarSystem
    {
        public mapSolarSystem()
        {
            securityClass = "";
            visualEffect = "";
            x = "";
            y = "";
            z = "";
            x2D = "";
            y2D = "";
        }

        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public long regionID {  get; set; }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public long constellationID { get; set; }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public long solarSystemID { get; set; }
        [Attributes.SQLiteType("INT")]
        public long factionID { get; set; }
        [Attributes.SQLIgnore()]
        public LanguageDescription name { get; set; }
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
        public List<double> planetIDs { get; set; }
        [Attributes.SQLiteType("INT")]
        public double radius { get; set; }
        [Attributes.SQLiteType("INT")]
        public bool regional { get; set; }
        [Attributes.SQLiteType("INT")]
        public double security { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string securityClass { get; set; }
        [Attributes.SQLiteType("INT")]
        public int sunTypeID {  get; set; }

        [Attributes.SQLIgnore() ]
        [Newtonsoft.Json.JsonConverter(typeof(SolarSystemPlanetConverter))]
        public List<mapPlanet> planets { get; set; }

        [Attributes.SQLiteType("INT")]
        public long starID { get; set; }

        [Attributes.SQLIgnore() ]
        [Newtonsoft.Json.JsonConverter(typeof(SolarSystemStargateConverter))]
        public List<SolarSystemStargate> stargates { get; set; }
        [Attributes.SQLIgnore() ]
        public LandmarkPosition position { get; set; }
        [Attributes.SQLIgnore() ]
        public LandmarkPosition position2D { get; set; }
        [Attributes.SQLiteType("INT")]
        public double securityStatus {  get; set; }
        [Attributes.SQLIgnore()]
        public List<long> stargateIDs { get; set; }
        [Attributes.SQLiteType("INT")]
        public int wormholeClassID { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string visualEffect {  get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string x { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string y { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string z { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string x2D { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string y2D { get; set; }
    }
}
