using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class TypeBonus
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public int typeBonusID {  get; set; }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int typeID { get; set; }
        [Attributes.SQLiteType("INT")]
        public double bonus {  get; set; }
        [Attributes.SQLIgnore()]
        public LanguageDescription bonusText { get; set; }
        [Attributes.SQLiteType("INT")]
        public int importance { get; set; }
        [Attributes.SQLiteType("INT")]
        public int unitID { get; set; }
        [Attributes.SQLiteType("INT")]
        public bool isRoleBonus { get; set; }



        [Attributes.SQLIgnore()]
        public List<TypeBonus> types { get; set; }
        [Attributes.SQLIgnore()]
        public List<TypeBonusRoleBonus> roleBonuses { get; set; }
        [Attributes.SQLIgnore()]
        [Newtonsoft.Json.JsonProperty("_value")]
        public List<TypeBonusRoleBonus> values { get; set; }
    }
}
