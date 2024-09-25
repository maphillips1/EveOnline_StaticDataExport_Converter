using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes.Converters;

namespace EveStaticDataExportConverter.Classes.FSD
{
    internal class ContrabandType
    {
        public ContrabandType() 
        {
            factions = new List<ContrabandType>();
        }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int contrabandTypeID {  get; set; }

        [Attributes.SQLiteType("INT")]
        public long factionID { get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal attackMinSec { get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal confiscateMinSec { get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal fineByValue { get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal standingLoss { get; set; }

        [Attributes.SQLIgnore()]
        [Newtonsoft.Json.JsonConverter(typeof(ContrabandTypeConverter))]
        public List<ContrabandType> factions { get; set; }
    }
}
