using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
{
    internal class Skin
    {
        public Skin()
        {
            internalName = "";
        }
        [Attributes.SQLiteType("INT")]
        public int skinID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public bool allowCCPDevs { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string internalName { get; set; }
        [Attributes.SQLiteType("INT")]
        public bool isStructureSkin { get; set; }
        [Attributes.SQLiteType("INT")]
        public int skinMaterialID { get; set; }
        [Attributes.SQLIgnore()]
        public List<int> types { get; set; }
        [Attributes.SQLiteType("INT")]
        public bool visibleSerenity { get; set; }
        [Attributes.SQLiteType("INT")]
        public bool visibleTranquility { get; set; }
    }
}
