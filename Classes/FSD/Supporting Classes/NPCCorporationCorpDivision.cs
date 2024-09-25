using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes
{
    internal class NPCCorporationCorpDivision
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public long npcCorporationID { get; set; }
        [Attributes.SQLiteType("INT")]
        public int npcCorporationDivisionID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public int divisionNumber { get; set; }
        [Attributes.SQLiteType("INT")]
        public long leaderID { get; set; }
        [Attributes.SQLiteType("INT")]
        public int size { get; set; }
    }
}
