using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.Universe.Supporting_Classes
{
    internal class SolarSystemStar
    {
        [Attributes.SQLiteType("INT")]
        public long solarSystemID {  get; set; }
        [Attributes.SQLiteType("INT")]
        [Newtonsoft.Json.JsonProperty("id")]
        public int starID { get; set; }
        [Attributes.SQLiteType("INT")]
        public double radius { get; set; }
        [Attributes.SQLiteType("INT")]
        public int typeID { get; set; }
        [Attributes.SQLIgnore()]
        public StarStatistics statistics { get; set; }

    }
}
