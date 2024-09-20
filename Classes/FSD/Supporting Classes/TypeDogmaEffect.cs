using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes
{
    internal class TypeDogmaEffect
    {
        [Attributes.SQLiteType("INT")]
        public int typeID { get; set; }
        [Attributes.SQLiteType("INT")]
        public int effectID { get; set; }
        [Attributes.SQLiteType("INT")]
        public bool isDefault { get; set; }
    }
}
