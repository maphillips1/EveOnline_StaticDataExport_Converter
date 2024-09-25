using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes
{
    internal class TournamentRuleSetBannedGroup
    {
        [Attributes.SQLiteType("TEXT")]
        [Attributes.SQLiteIndex()]
        public string ruleSetID {  get; set; }

        [Attributes.SQLiteType("INT")]
        public int groupID { get; set; }
    }
}
