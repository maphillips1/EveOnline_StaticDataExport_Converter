using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
{
    internal class TournamentRuleSet
    {
        public TournamentRuleSet()
        {
            ruleSetID = "";
            ruleSetName = "";
        }

        [Attributes.SQLiteType("TEXT")]
        [Attributes.SQLiteIndex()]
        public string ruleSetID { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string ruleSetName { get; set; }

        [Attributes.SQLIgnore()]
        public TournamentRuleSetBanned banned {  get; set; }

        [Attributes.SQLiteType("INT")]
        public int maximumPilotsMatch { get; set; }

        [Attributes.SQLiteType("INT")]
        public int maximumPointsMatch { get; set; }

        [Attributes.SQLIgnore()]
        public TournamentRuleSetPoints points {  get; set; }
    }
}
