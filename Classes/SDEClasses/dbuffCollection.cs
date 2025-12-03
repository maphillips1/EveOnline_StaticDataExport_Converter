using EveStaticDataExportConverter.Classes.SDEClasses;
using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class dbuffCollection
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public int dbuffCollectionId { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string aggregateMode { get; set; }


        [Attributes.SQLiteType("TEXT")]
        public string developerDescription { get; set; }

        [Attributes.SQLIgnore()]
        public List<DBuffCollectionDogmaAttribute> itemModifiers { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription displayName { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string operationName { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string showOutputValueInUI { get; set; }
    }
}
