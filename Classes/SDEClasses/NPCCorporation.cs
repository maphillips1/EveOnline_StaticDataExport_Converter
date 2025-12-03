using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class NPCCorporation
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public long npcCorporationID {  get; set; }

        [Attributes.SQLIgnore()]
        public List<int> allowedMemberRaces { get; set; }

        [Attributes.SQLIgnore()]
        public List<NPCCorpAllowedRace> allowedRaces { get; set; }

        [Attributes.SQLiteType("INT")]
        public long ceoID { get; set; }

        [Attributes.SQLIgnore()]
        public List<CorporationTrade> corporationTrades { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool deleted { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription description { get; set; }

        [Attributes.SQLIgnore()]
        public List<Supporting_Classes.NPCCorporationCorpDivision> divisions { get; set; }

        [Attributes.SQLiteType("INT")]
        public long enemyID { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string extent {  get; set; }

        [Attributes.SQLiteType("INT")]
        public long factionID { get; set; }

        [Attributes.SQLiteType("INT")]
        public long friendID { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool hasPlayerPersonnelManager { get; set; }

        [Attributes.SQLiteType("INT")]
        public int iconID { get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal initialPrice { get; set; }

        [Attributes.SQLIgnore()]
        public List<int> lpOfferTables { get; set; }

        [Attributes.SQLIgnore()]
        public List<NPCCorpLPOfferTable> corpLPOfferTables { get; set; }

        [Attributes.SQLiteType("INT")]
        public int mainActivityID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int memberLimit { get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal minSecurity {  get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal minimumJoinStanding { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription name { get; set; }

        [Attributes.SQLiteType("INT")]
        public long publicShares { get; set; }

        [Attributes.SQLiteType("INT")]
        public int raceID { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool sendCharTerminationMessage { get; set; }

        [Attributes.SQLiteType("INT")]
        public long shares { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string size { get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal sizeFactor { get; set; }

        [Attributes.SQLiteType("INT")]
        public long solarSystemID { get; set; }

        [Attributes.SQLiteType("INT")]
        public long stationID { get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal taxRate { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string tickerName { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool uniqueName { get; set; }
    }
}
