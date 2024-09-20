using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes;
using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
{
    internal class PlanetSchematic
    {
        [Attributes.SQLiteType("INT")]
        public int planetSchematicID {  get; set; }

        [Attributes.SQLiteType("INT")]
        public int cycleTime { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription nameID { get; set; }

        [Attributes.SQLIgnore()]
        public List<int> pins {  get; set; }

        [Attributes.SQLIgnore()]
        public List<PlanetSchematicPin> planetSchematicPins { get; set; }

        [Attributes.SQLIgnore()]
        [Newtonsoft.Json.JsonConverter(typeof(PlanetSchematicTypeConverter))]
        public List<PlanetSchematicType> types { get; set; }
    }
}
