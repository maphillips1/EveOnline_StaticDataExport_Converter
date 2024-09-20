using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes
{
    internal class TournamentRuleSetPointsType
    {
        [Attributes.SQLiteType("TEXT")]
        public string ruleSetID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int points { get; set; }

        [Attributes.SQLiteType("INT")]
        public int typeID { get; set; }
    }
}
