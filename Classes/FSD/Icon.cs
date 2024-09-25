using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
{
    internal class Icon
    {
        public Icon()
        {
            description = "";
            iconFile = "";
        }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int iconID {  get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string description { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string iconFile { get; set; }
    }
}
