using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.Universe
{
    internal class Region
    {
        [Attributes.SQLiteType("INT")]
        public long regionID {  get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string regionName { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string regionTypeName { get; set; }
        [Attributes.SQLIgnore()]
        public List<double> center {  get; set; }
        [Attributes.SQLiteType("INT")]
        public int descriptionID { get; set; }
        [Attributes.SQLIgnore()]
        public List<double> max {  get; set; }
        [Attributes.SQLIgnore()]
        public List<double> min { get; set; }
        [Attributes.SQLiteType("INT")]
        public int nameID { get; set; }
        [Attributes.SQLiteType("INT")]
        public int nebula {  get; set; }
        [Attributes.SQLiteType("INT")]
        public int wormholeClassID { get; set; }

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
