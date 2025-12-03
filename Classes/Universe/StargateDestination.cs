using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.Universe
{
    internal class StargateDestination
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public long stargateID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public long solarSystemID { get; set; }
        [Attributes.SQLiteType("INT")]
        [Newtonsoft.Json.JsonProperty("stargateID")]
        public long destinationStargateID { get; set; }

    }
}
