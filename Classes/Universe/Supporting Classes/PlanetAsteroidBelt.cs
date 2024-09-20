using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.Universe.Supporting_Classes
{
    internal class PlanetAsteroidBelt
    {
        [Attributes.SQLiteType("INT")]
        public long planetID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public long asteroidBeltID { get; set; }
        [Attributes.SQLIgnore()]
        public List<double> position { get; set; }
        [Attributes.SQLIgnore()]
        public AsteroidBetlStatistics statistics { get; set; }
        [Attributes.SQLiteType("INT")]
        public int typeID { get; set; }

        [Attributes.SQLiteType("INT")]
        public double positionX { get; set; }
        [Attributes.SQLiteType("INT")]
        public double positionY { get; set; }
        [Attributes.SQLiteType("INT")]
        public double positionZ { get; set; }
    }
}
