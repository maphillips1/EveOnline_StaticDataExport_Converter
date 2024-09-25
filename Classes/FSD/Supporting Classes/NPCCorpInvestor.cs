using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes
{
    internal class NPCCorpInvestor
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public long npcCorporationID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public long investorID { get; set; }
        [Attributes.SQLiteType("INT")]
        public int investorAmount { get; set; }
    }
}
