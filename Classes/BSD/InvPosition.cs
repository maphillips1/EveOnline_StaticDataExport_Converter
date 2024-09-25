using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.BSD
{
    internal class InvPosition
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public long itemID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public double pitch { get; set; }
        [Attributes.SQLiteType("INT")]
        public double roll { get; set; }
        [Attributes.SQLiteType("INT")]
        public double x { get; set; }
        [Attributes.SQLiteType("INT")]
        public double y { get; set; }
        [Attributes.SQLiteType("INT")]
        public double yaw { get; set; }
        [Attributes.SQLiteType("INT")]
        public double z { get; set; }
    }
}
