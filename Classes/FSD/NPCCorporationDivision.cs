using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
{
    internal class NPCCorporationDivision
    {
        public NPCCorporationDivision() 
        {
            description = "";
            internalName = "";
        }
        [Attributes.SQLiteType("INT")]
        public int npcCorporationDivisionID {  get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string description { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string internalName { get; set; }
        [Attributes.SQLIgnore()]
        public LanguageDescription leaderTypeNameID { get; set; }
        [Attributes.SQLIgnore()]
        public LanguageDescription nameID { get; set; }
    }
}
