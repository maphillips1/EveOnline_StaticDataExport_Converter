using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.Attributes
{
    abstract class ClassWithDescription : KeyedObject
    {

        [Newtonsoft.Json.JsonProperty("description")]
        public abstract LanguageDescription description { get; set; }

        public abstract string GetCategory();

        public abstract long GetKey1();

        public abstract long GetKey2();
    }
}
