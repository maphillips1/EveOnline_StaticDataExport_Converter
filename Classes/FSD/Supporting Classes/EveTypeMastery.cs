using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes
{
    internal class EveTypeMastery
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int typeID { get; set; }
        [Attributes.SQLiteType("INT")]
        public int masteryID { get; set; }
        [Attributes.SQLIgnore()]
        public List<int> masteryIDs { get; set; }
        [Attributes.SQLiteType("INT")]
        public int masteryTypeId { get; set; }
    }
}
