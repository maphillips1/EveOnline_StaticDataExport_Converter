using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes
{
    internal class TypeBonusRoleBonus
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int typeBonusID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public double bonus {  get; set; }
        [Attributes.SQLIgnore()]
        public LanguageDescription bonusText { get; set; }
        [Attributes.SQLiteType("INT")]
        public int importance { get; set; }
        [Attributes.SQLiteType("INT")]
        public int unitID { get; set; }
    }
}
