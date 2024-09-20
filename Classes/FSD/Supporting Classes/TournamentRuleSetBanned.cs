using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes
{
    internal class TournamentRuleSetBanned
    {
        [Attributes.SQLiteType("TEXT")]
        public string ruleSetID {  get; set; }

        [Attributes.SQLiteType("INT")]
        public List<int> groups {  get; set; }

        [Attributes.SQLiteType("INT")]
        public List<int> types { get; set; }
    }
}
