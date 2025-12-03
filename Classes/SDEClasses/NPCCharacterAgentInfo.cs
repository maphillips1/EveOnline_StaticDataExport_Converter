using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class NPCCharacterAgentInfo
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int npcCharacterID { get; set; }
        [Attributes.SQLiteType("INT")]
        public int agentTypeID { get; set; }
        [Attributes.SQLiteType("INT")]
        public int divisionID { get; set; }
        [Attributes.SQLiteType("INT")]
        public bool isLocator { get; set; }
        [Attributes.SQLiteType("INT")]
        public int level { get; set; }
    }
}
