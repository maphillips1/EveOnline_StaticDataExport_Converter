using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.Universe.Supporting_Classes
{
    internal class RegionConstellation
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public long regionID { get; set; }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public long constellationID { get; set; }
    }
}
