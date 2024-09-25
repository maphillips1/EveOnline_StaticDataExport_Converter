using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.Universe.Supporting_Classes
{
    internal class PlanetAttributes

    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public long planetID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public int heightMap1 { get; set; }
        [Attributes.SQLiteType("INT")]
        public int heightMap2 { get; set; }
        [Attributes.SQLiteType("INT")]
        public bool population {  get; set; }
        [Attributes.SQLiteType("INT")]
        public int shaderPreset {  get; set; }
    }
}
