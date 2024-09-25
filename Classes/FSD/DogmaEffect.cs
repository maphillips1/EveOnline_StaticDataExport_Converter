using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
{
    internal class DogmaEffect
    {
        public DogmaEffect() 
        {
            guid = "";
        }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int dogmaEffectID {  get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription descriptionID { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool disallowAutoRepeat { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription displayNameID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int effectCategory { get; set; }

        [Attributes.SQLIgnore()]
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

        [Attributes.SQLiteType("INT")]
        public bool propulsionChance { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool published { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool rangeChance { get; set; }
    }
}
