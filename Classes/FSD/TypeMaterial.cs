using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
{
    internal class TypeMaterial
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int typeID { get; set; }
        [Attributes.SQLIgnore()]
        public List<TypeMaterial> materials { get; set; }
        [Attributes.SQLiteType("INT")]
        public int materialTypeID { get; set; }
        [Attributes.SQLiteType("INT")]
        public int quantity { get; set; }
    }
}
