using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class Icon
    {
        public Icon()
        {
            iconFile = "";
        }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public int iconID {  get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string iconFile { get; set; }
    }
}
