using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class ResearchAgent
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int researchAgentID {  get; set; }

        [Attributes.SQLiteType("INT")]
        public int skillTypeID { get; set; }

        [Attributes.SQLIgnore()]
        public List<ResearchAgentSkillType> skills { get; set; }

    }
}
