using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes
{
    internal class CertificateRecommendedForType
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int certificateID {  get; set; }
        [Attributes.SQLiteType("INT")]
        [Newtonsoft.Json.JsonProperty("_key")]
        public int recommendedForTypeID { get; set; }
    }
}
