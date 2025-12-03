using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class DogmaEffect
    {
        public DogmaEffect() 
        {
            guid = "";
        }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public int dogmaEffectID {  get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription description { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool disallowAutoRepeat { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription displayName { get; set; }

        [Attributes.SQLiteType("INT")]
        public int effectCategoryID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int effectID { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool electronicChance { get; set; }

        [Attributes.SQLiteType("INT")]
        public string guid { get; set; }

        [Attributes.SQLiteType("INT")]
        public int iconID { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool isAssistance { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool isOffensive { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool isWarpSafe { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string name { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool propulsionChance { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool published { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool rangeChance { get; set; }
    }
}
