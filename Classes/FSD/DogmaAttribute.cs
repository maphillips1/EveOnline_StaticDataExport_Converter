using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
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
        public int attributeID {  get; set; }

        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int categoryID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int dataType { get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal defaultValue { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string description { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription displayNameID { get; set; }

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

        [Attributes.SQLIgnore()]
        public LanguageDescription tooltipDescriptionID { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription tooltipTitleID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int unitID { get; set; }
    }
}
