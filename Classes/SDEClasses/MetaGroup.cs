using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class MetaGroup
    {
        public MetaGroup() 
        {
            iconSuffix = "";
        }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public int metaGroupID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public int iconID { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string iconSuffix { get; set; }
        [Attributes.SQLIgnore()]
        public LanguageDescription description { get; set; }
        [Attributes.SQLIgnore()]
        public LanguageDescription name { get; set; }
        [Attributes.SQLIgnore()]
        public BasicColor color { get; set; }
        [Attributes.SQLiteType("INT")]
        public double r { get; set; }
        [Attributes.SQLiteType("INT")]
        public double g { get; set; }
        [Attributes.SQLiteType("INT")]
        public double b { get; set; }
    }
}
