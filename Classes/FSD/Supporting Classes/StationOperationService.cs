using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes
{
    internal class StationOperationService
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int stationOperationID { get; set; }
        [Attributes.SQLiteType("INT")]
        public int stationServiceID { get; set; }
    }
}
