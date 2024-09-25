using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.Universe
{
    internal class Constellation
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public long regionID {  get; set; }

        [Attributes.SQLiteIndex()]
        [Attributes.SQLiteType("INT")]
        public long constellationID {  get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string constellationName { get; set; }

        [Attributes.SQLIgnore()]
        public List<double> center {  get; set; }

        [Attributes.SQLIgnore()]
        public List<double> max {  get; set; }

        [Attributes.SQLIgnore()]
        public List<double> min { get; set; }

        [Attributes.SQLiteType("INT")]
        public int nameID { get; set; }

        [Attributes.SQLiteType("INT")]
        public double radius { get; set; }


        [Attributes.SQLiteType("INT")]
        public double centerX { get; set; }
        [Attributes.SQLiteType("INT")]
        public double centerY { get; set; }
        [Attributes.SQLiteType("INT")]
        public double centerZ { get; set; }

        [Attributes.SQLiteType("INT")]
        public double maxX { get; set; }
        [Attributes.SQLiteType("INT")]
        public double maxY { get; set; }
        [Attributes.SQLiteType("INT")]
        public double maxZ { get; set; }

        [Attributes.SQLiteType("INT")]
        public double minX { get; set; }
        [Attributes.SQLiteType("INT")]
        public double minY { get; set; }
        [Attributes.SQLiteType("INT")]
        public double minZ { get; set; }
    }
}
