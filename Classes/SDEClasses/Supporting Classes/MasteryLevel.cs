using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes
{
    internal class MasteryLevel
    {
        [Newtonsoft.Json.JsonProperty("_key")]
        public int level {  get; set; }
        public List<int> _value { get; set; }
    }
}
