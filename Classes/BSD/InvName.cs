using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.BSD
{
    internal class InvName
    {
        public InvName()
        {
            itemName = "";
        }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public long itemID {  get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string itemName { get; set; }

    }
}
