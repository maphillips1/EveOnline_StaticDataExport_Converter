using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
{
    internal class SkinMaterial
    {
        [Attributes.SQLiteType("INT")]
        public int skinMaterialID { get; set; }
        [Attributes.SQLiteType("INT")]
        public int displayNameID { get; set; }
        [Attributes.SQLiteType("INT")]
        public int materialSetID { get; set; }
    }
}
