using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes
{
    internal class TournamentRuleSetBannedType
    {
        [Attributes.SQLiteType("TEXT")]
        [Attributes.SQLiteIndex()]
        public string ruleSetID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int typeID { get; set; }
    }
}
