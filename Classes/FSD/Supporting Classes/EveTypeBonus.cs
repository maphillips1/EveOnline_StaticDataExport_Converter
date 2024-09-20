using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes
{
    internal class EveTypeBonus
    {
        [Attributes.SQLiteType("INT")]
        public int typeID {  get; set; }

        [Attributes.SQLiteType("INT")]
        public int bonusedTypeID { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription bonusText { get; set; }

        [Attributes.SQLiteType("INT")]
        public int importance {  get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal bonus {  get; set; }

        [Attributes.SQLiteType("INT")]
        public int unitID { get; set; }
    }
}
