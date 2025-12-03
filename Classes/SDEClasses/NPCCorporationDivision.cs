using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class NPCCorporationDivision
    {
        public NPCCorporationDivision() 
        {
            internalName = "";
            displayName = "";
        }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public int npcCorporationDivisionID {  get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string displayName { get; set; }
        [Attributes.SQLIgnore()]
        public LanguageDescription description { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string internalName { get; set; }
        [Attributes.SQLIgnore()]
        public LanguageDescription leaderTypeName { get; set; }
        [Attributes.SQLIgnore()]
        public LanguageDescription name { get; set; }
    }
}
