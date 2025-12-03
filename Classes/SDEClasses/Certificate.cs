using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class Certificate
    {
        public Certificate() 
        {
            description = new LanguageDescription();
            name = new LanguageDescription();
            recommendedFor = new List<int>();
            skillTypes = new List<CertificateSkillType>();
        }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public int certificateID {  get; set; }

        [Attributes.SQLIgnore]
        public LanguageDescription description { get; set; }

        [Attributes.SQLiteType("INT")]
        public int groupID { get; set; }

        [Attributes.SQLIgnore]
        public LanguageDescription name { get; set; }

        [Attributes.SQLiteType("TEXT")]

        [Attributes.SQLIgnore()]
        public List<int> recommendedFor {  get; set; }

        [Attributes.SQLIgnore()]
        public List<CertificateSkillType> skillTypes { get; set; }
    }
}
