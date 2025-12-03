using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class StationOperation
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public int stationOperationID {  get; set; }

        [Attributes.SQLiteType("INT")]
        public int activityID { get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal border {  get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal corridor { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription description { get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal fringe {  get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal hub { get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal manufacturingFactor { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription operationName { get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal ratio { get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal researchFactor { get; set; }

        [Attributes.SQLIgnore()]
        public List<int> services { get; set; }

        [Attributes.SQLIgnore()]
        public List<StationType> stationTypes { get; set; }

    }
}
