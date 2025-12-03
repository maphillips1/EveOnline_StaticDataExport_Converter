using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class DogmaUnit
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public int dogmaUnitId { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription description { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription displayName { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string name { get; set; }
    }
}
