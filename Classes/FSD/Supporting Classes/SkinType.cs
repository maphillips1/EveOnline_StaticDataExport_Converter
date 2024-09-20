using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes
{
    internal class SkinType
    {
        [Attributes.SQLiteType("INT")]
        public int skinID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public int typeID { get; set; }

    }
}
