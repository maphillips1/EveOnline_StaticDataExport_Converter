using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EveStaticDataExportConverter.Classes.FSD
{
    internal class Ancestry
    {
        internal Ancestry()
        {
            this.descriptionID = new LanguageDescription();
            this.nameID = new LanguageDescription();
            this.shortDescription = "";
        }
        [Attributes.SQLiteType("INT")]
        public int ancestryID {  get; set; }

        [Attributes.SQLiteType("INT")]
        public int bloodlineID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int charisma { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription descriptionID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int iconID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int intelligence { get; set; }

        [Attributes.SQLiteType("INT")]
        public int memory {  get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription nameID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int perception { get; set; }

        [Attributes.SQLiteType("VARCHAR (255)")]
        public string shortDescription { get; set; }

        [Attributes.SQLiteType("INT")]
        public int willpower { get; set; }
    }
}
