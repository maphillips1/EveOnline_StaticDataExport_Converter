using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class PlanetSchematic
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public int planetSchematicID {  get; set; }

        [Attributes.SQLiteType("INT")]
        public int cycleTime { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription name { get; set; }

        [Attributes.SQLIgnore()]
        public List<int> pins {  get; set; }

        [Attributes.SQLIgnore()]
        public List<PlanetSchematicPin> planetSchematicPins { get; set; }

        [Attributes.SQLIgnore()]
        public List<PlanetSchematicType> types { get; set; }
    }
}
