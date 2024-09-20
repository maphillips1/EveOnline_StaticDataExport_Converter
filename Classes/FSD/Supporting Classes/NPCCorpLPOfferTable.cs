using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes
{
    internal class NPCCorpLPOfferTable
    {
        [Attributes.SQLiteType("INT")]
        public long npcCorporationID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public int lpOfferTableID { get; set; }
    }
}
