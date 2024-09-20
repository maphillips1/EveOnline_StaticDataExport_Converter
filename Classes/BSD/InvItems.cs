using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.BSD
{
    internal class InvItems
    {
        [Attributes.SQLiteType("INT")]
        public long itemID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public int flagID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public long locationID { get; set; }
        [Attributes.SQLiteType("INT")]
        public int ownerID { get; set; }
        [Attributes.SQLiteType("INT")]
        public decimal quantity { get; set; }
        [Attributes.SQLiteType("INT")]
        public int typeID { get; set; }
    }
}
