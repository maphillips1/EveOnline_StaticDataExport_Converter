using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class TypeDogma
    {
        [Attributes.SQLiteType("INT")]
        [Newtonsoft.Json.JsonProperty("_key")]
        public int typeID { get; set; }

        [Attributes.SQLIgnore()]
        public List<TypeDogmaAttribute> dogmaAttributes { get; set; }

        [Attributes.SQLIgnore()]
        public List<TypeDogmaEffect> dogmaEffects { get; set; }

    }
}
