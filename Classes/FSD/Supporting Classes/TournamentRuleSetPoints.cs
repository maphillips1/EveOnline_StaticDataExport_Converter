using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes
{
    internal class TournamentRuleSetPoints
    {
        public string ruleSetID { get; set; }
        public List<TournamentRuleSetPointsGroup> groups {  get; set; }
        public List<TournamentRuleSetPointsType> types {  get; set; }
    }
}
