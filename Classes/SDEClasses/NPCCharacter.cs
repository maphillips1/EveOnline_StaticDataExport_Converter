using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class NPCCharacter
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public int npcCharacterID {  get; set; }
        [Attributes.SQLIgnore()]
        public NPCCharacterAgentInfo agent {  get; set; }
        [Attributes.SQLiteType("INT")]
        public int ancestryID { get; set; }
        [Attributes.SQLiteType("INT")]
        public int bloodlineID { get; set; }
        [Attributes.SQLiteType("INT")]
        public int careerID { get; set; }
        [Attributes.SQLiteType("INT")]
        public bool ceo { get; set; }
        [Attributes.SQLiteType("INT")]
        public long corporationID { get; set; }
        [Attributes.SQLiteType("INT")]
        public bool gender { get; set; }
        [Attributes.SQLiteType("INT")]
        public long locationID { get; set; }
        [Attributes.SQLIgnore()]
        public LanguageDescription name { get; set; }
        [Attributes.SQLiteType("INT")]
        public int raceID { get; set; }
        [Attributes.SQLIgnore()]
        public List<NPCCharacterSkill> skills { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public DateTime startDate { get; set; }
        [Attributes.SQLiteType("INT")]
        public bool uniqueName { get; set; }

    }
}
