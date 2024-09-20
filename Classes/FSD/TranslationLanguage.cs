using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
{
    internal class TranslationLanguage
    {
        [Attributes.SQLiteType("INT")]
        public int transalationLanguageID { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string de {  get; set; }
        [Attributes.SQLiteType("TEXT")]
        [Newtonsoft.Json.JsonProperty("en-us")]
        public string en_us { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string es { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string fre { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string it { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string ja { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string ko { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string ru { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string zh { get; set; }
    }
}
