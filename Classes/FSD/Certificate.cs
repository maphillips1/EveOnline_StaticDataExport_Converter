using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes;
using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
{
    internal class Certificate
    {
        public Certificate() 
        {
            description = "";
            name = "";
            recommendedFor = new List<int>();
            skillTypes = new List<CertificateSkillType>();
        }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int certificateID {  get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string description { get; set; }

        [Attributes.SQLiteType("INT")]
        public int groupID { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string name { get; set; }

        [Attributes.SQLiteType("TEXT")]

        [Attributes.SQLIgnore()]
        public List<int> recommendedFor {  get; set; }

        [Attributes.SQLIgnore()]
        [Newtonsoft.Json.JsonConverter(typeof(CertificateSkillTypeConverter))]
        public List<CertificateSkillType> skillTypes { get; set; }
    }
}
