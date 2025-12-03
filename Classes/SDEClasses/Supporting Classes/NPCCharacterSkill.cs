using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes
{
    internal class NPCCharacterSkill
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int npcCharacterID { get; set; }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int typeID { get; set; }
    }
}
