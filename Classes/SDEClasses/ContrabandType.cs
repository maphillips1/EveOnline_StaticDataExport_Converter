using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class ContrabandType
    {
        public ContrabandType() 
        {
            factions = new List<ContrabandTypeFaction>();
        }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
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
        public List<ContrabandTypeFaction> factions { get; set; }
    }


    internal class ContrabandTypeFaction
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int contrabandTypeID { get; set; }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public long factionID { get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal attackMinSec { get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal confiscateMinSec { get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal fineByValue { get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal standingLoss { get; set; }
    }
}