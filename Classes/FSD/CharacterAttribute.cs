using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
{
    internal class CharacterAttribute
    {
        public CharacterAttribute() 
        {
            description = "";
            notes = "";
            shortDescription = "";
            nameID = new LanguageDescription();
        }
        [Attributes.SQLiteType("INT")]
        public int characterAttributeID {  get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string description { get; set; }

        [Attributes.SQLiteType("INT")]
        public int iconID { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription nameID { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string notes { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string shortDescription { get; set; }
    }
}
