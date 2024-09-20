using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.Universe.SupportingClasses
{
    internal class SolarSystemStargate
    {
        [Attributes.SQLiteType("INT")]
        public long solarSystemID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public long stargateID { get; set; }
        [Attributes.SQLiteType("INT")]
        public long destination { get; set; }
        [Attributes.SQLIgnore()]
        public List<double> position {  get; set; }
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
