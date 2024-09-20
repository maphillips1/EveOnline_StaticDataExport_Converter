using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
{
    internal class MarketGroup
    {
        [Attributes.SQLiteType("INT")]
        public int marketGroupId {  get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription descriptionID { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool hasTypes { get; set; }

        [Attributes.SQLiteType("INT")]
        public int iconID { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription nameID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int parentGroupID {  get; set; }
    }
}
