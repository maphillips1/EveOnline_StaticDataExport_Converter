using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes
{
    internal class CertificateSkillType
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int certificateID {  get; set; }
        [Attributes.SQLiteType("INT")]
        [Newtonsoft.Json.JsonProperty("_key")]
        public int skillTypeID { get; set; }
        [Attributes.SQLiteType("INT")]
        public int advanced {  get; set; }
        [Attributes.SQLiteType("INT")]
        public int basic {  get; set; }
        [Attributes.SQLiteType("INT")]
        public int elite { get; set; }
        [Attributes.SQLiteType("INT")]
        public int improved { get; set; }
        [Attributes.SQLiteType("INT")]
        public int standard {  get; set; }
    }
}
