using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class CharacterAttribute
    {
        public CharacterAttribute() 
        {
            description = "";
            notes = "";
            shortDescription = "";
            name = new LanguageDescription();
        }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public int characterAttributeID {  get; set; }


        [Attributes.SQLiteType("TEXT")]
        public string description { get; set; }

        [Attributes.SQLiteType("INT")]
        public int iconID { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription name { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string notes { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string shortDescription { get; set; }
    }
}
