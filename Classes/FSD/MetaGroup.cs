using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
{
    internal class MetaGroup
    {
        public MetaGroup() 
        {
            iconSuffix = "";
        }
        [Attributes.SQLiteType("INT")]
        public int metaGroupID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public int iconID { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string iconSuffix { get; set; }
        [Attributes.SQLIgnore()]
        public LanguageDescription nameID { get; set; }
    }
}
