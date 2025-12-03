using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class DogmaAttributeCategory
    {
        public DogmaAttributeCategory() 
        {
            description = "";
            name = "";
        }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public int dogmaAttributeCategoryID { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string description { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string name { get; set; }
    }
}
