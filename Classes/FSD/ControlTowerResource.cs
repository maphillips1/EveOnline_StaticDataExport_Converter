using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
{
    internal class ControlTowerResource
    {
        public ControlTowerResource() 
        {
            this.resources = new List<ControlTowerResource>();
        }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int controlTowerResourceID {  get; set; }

        [Attributes.SQLIgnore()]
        [Newtonsoft.Json.JsonConverter(typeof(ControlTowerResourceConverter))]
        public List<ControlTowerResource> resources { get; set; }

        [Attributes.SQLiteType("INT")]
        public long factionID { get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal minSecurityLevel { get; set; }

        [Attributes.SQLiteType("INT")]
        public int purpose { get; set; }

        [Attributes.SQLiteType("INT")]
        public int quantity { get; set; }

        [Attributes.SQLiteType("INT")]
        public int resourceTypeID { get; set; }
    }
}
