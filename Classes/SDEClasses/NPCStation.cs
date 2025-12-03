using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class NPCStation
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public int npcStationID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public int celestialIndex { get; set; }
        [Attributes.SQLiteType("INT")]
        public int operationID { get; set; }
        [Attributes.SQLiteType("INT")]
        public long orbitID { get; set; }
        [Attributes.SQLiteType("INT")]
        public int orbitIndex { get; set; }
        [Attributes.SQLiteType("INT")]
        public long ownerID { get; set; }
        [Attributes.SQLIgnore()]
        public LandmarkPosition position { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string x {  get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string y { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string z { get; set; }
        [Attributes.SQLiteType("INT")]
        public double reprocessingEfficiency { get; set; }
        [Attributes.SQLiteType("INT")]
        public int reprocessingHangarFlag { get; set; }
        [Attributes.SQLiteType("INT")]
        public double reprocessingStationsTake { get; set; }
        [Attributes.SQLiteType("INT")]
        public long solarSystemID { get; set; }
        [Attributes.SQLiteType("INT")]
        public int typeID { get; set; }
        [Attributes.SQLiteType("INT")]
        public bool useOperationName { get; set; }
    }
}
