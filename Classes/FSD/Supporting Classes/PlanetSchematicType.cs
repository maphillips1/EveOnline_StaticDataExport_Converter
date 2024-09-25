using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes
{
    internal class PlanetSchematicType
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int planetSchematicID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public int planetSchematicTypeID { get; set; }
        [Attributes.SQLiteType("INT")]
        public bool isInput {  get; set; }
        [Attributes.SQLiteType("INT")]
        public int quantity { get; set; }
    }
}
