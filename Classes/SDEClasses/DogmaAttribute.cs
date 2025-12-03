using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class DogmaAttribute
    {
        public DogmaAttribute() 
        {
            description = "";
            name = "";
        }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public int attributeID {  get; set; }

        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int attributeCategoryID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int dataType { get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal defaultValue { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string description { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription displayName { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool displayWhenZero { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool highIsGood { get; set; }

        [Attributes.SQLiteType("INT")]
        public int iconID { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string name { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool published { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool stackable { get; set; }

        [Attributes.SQLiteType("INT")]
        public int unitID { get; set; }
    }
}
