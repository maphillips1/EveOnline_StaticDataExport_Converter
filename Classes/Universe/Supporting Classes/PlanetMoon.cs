using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.Universe.Supporting_Classes
{
    internal class PlanetMoon
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public long planetID {  get; set; }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public long moonID { get; set; }
        [Attributes.SQLIgnore()]
        public MoonAttributes planetAttributes { get; set; }
        [Attributes.SQLIgnore()]
        public List<double> position { get; set; }
        [Attributes.SQLiteType("INT")]
        public double radius { get; set; }
        [Attributes.SQLiteType("INT")]
        public int typeID { get; set; }

        [Attributes.SQLiteType("INT")]
        public double positionX { get; set; }
        [Attributes.SQLiteType("INT")]
        public double positionY { get; set; }
        [Attributes.SQLiteType("INT")]
        public double positionZ { get; set; }
        [Attributes.SQLIgnore() ]
        public MoonStatistics statistics { get; set; }
    }
}
