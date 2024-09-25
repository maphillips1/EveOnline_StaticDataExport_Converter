using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.BSD
{
    internal class InvFlag
    {
        public InvFlag()
        {
            flagName = "";
            flagText = "";
        }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int flagID {  get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string flagName { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string flagText { get; set; }
        [Attributes.SQLiteType("INT")]
        public int orderID { get; set; }
    }
}
