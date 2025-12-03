using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.Universe.Supporting_Classes
{
    internal class mapStar
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public long solarSystemID {  get; set; }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public int starID { get; set; }
        [Attributes.SQLiteType("INT")]
        public double radius { get; set; }
        [Attributes.SQLiteType("INT")]
        public int typeID { get; set; }
        [Attributes.SQLIgnore()]
        public StarStatistics statistics { get; set; }

    }
}
