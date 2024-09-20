using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes
{
    internal class StationType
    {
        [Attributes.SQLiteType("INT")]
        public int stationOperationID { get; set; }
        [Attributes.SQLiteType("INT")]
        public int stationTypeID { get; set; }
        [Attributes.SQLiteType("INT")]
        public int typeID { get; set; }
    }
}
