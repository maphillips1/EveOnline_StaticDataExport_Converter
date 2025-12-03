using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes
{
    internal class StationType
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int stationOperationID { get; set; }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public int stationTypeID { get; set; }
        [Attributes.SQLiteType("INT")]
        [Newtonsoft.Json.JsonProperty("_value")]
        public int typeID { get; set; }
    }
}
