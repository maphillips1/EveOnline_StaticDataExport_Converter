using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class TranslationLanguage
    {
        [Attributes.SQLiteType("TEXT")]
        [Newtonsoft.Json.JsonProperty("_key")]
        public string shortName { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string name { get; set; }
    }
}
