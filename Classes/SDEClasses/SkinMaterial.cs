using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class SkinMaterial
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public int skinMaterialID { get; set; }
        [Attributes.SQLIgnore()]
        public LanguageDescription displayName { get; set; }
        [Attributes.SQLiteType("INT")]
        public int materialSetID { get; set; }
    }
}
