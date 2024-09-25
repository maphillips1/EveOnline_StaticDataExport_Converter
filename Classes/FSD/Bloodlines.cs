using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
{
    internal class Bloodlines
    {
        public Bloodlines() {
            descriptionID = new LanguageDescription();
            nameID = new LanguageDescription();
        }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int bloodlinesID {  get; set; }

        [Attributes.SQLiteType("INT")]
        public int charisma {  get; set; }

        [Attributes.SQLiteType("INT")]
        public int corporationID { get; set; }


        [Attributes.SQLIgnore()]
        public LanguageDescription descriptionID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int iconID {  get; set; }

        [Attributes.SQLiteType("INT")]
        public int intelligence {  get; set; }

        [Attributes.SQLiteType("INT")]
        public int memory {  get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription nameID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int perception {  get; set; }

        [Attributes.SQLiteType("INT")]
        public int raceID { get; set; }
        
        [Attributes.SQLiteType("INT")]
        public int willpower { get; set; }
    }
}
