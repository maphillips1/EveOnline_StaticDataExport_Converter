using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.BSD
{
    internal class InvUniqueName
    {
        public InvUniqueName()
        {
            itemName = "";
        }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public long itemID { get; set; }
        [Attributes.SQLiteType("INT")]
        public int groupID {  get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string itemName { get; set; }
    }
}
