using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes
{
    internal class LanguageDescription
    {
        internal LanguageDescription() 
        {
            this.de = "";
            this.en = "";
            this.es = "";
            this.fr = "";
            this.ja = "";
            this.ko = "";
            this.ru = "";
            this.zh = "";
            parentTypeCategory = "";
        }
        [Attributes.SQLiteType("INT")]
        public long parentTypeId { get; set; }

        [Attributes.SQLiteType("INT")]
        public long parentTypeId2 { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string parentTypeCategory { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string de { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string en { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string es { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string fr { get; set; }
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
