using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes
{
    internal class TypeDogmaAttribute
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int typeID { get; set; }
        [Attributes.SQLiteType("INT")]
        public int attributeID { get; set; }
        [Attributes.SQLiteType("INT")]
        public decimal value { get; set; }
    }
}
