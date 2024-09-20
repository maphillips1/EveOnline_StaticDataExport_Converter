using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
{
    internal class EveGroup
    {
        [Attributes.SQLiteType("INT")]
        public int groupID {  get; set; }

        [Attributes.SQLiteType("INT")]
        public bool anchorable { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool anchored { get; set; }

        [Attributes.SQLiteType("INT")]
        public int categoryID { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool fittableNonSingleton { get; set; }

        [Attributes.SQLiteType("INT")]
        public int iconID { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription name { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool published { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool useBasePrice { get; set; }
    }
}
