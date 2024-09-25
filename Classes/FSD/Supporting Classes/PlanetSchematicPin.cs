using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes
{
    internal class PlanetSchematicPin
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int planetSchematicID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public int pinID { get; set; }
    }
}
