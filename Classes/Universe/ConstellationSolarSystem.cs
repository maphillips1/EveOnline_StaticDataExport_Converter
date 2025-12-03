using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.Universe
{
    internal class ConstellationSolarSystem
    {

        [Attributes.SQLiteIndex()]
        [Attributes.SQLiteType("INT")]
        public long constellationID { get; set; }

        [Attributes.SQLiteIndex()]
        [Attributes.SQLiteType("INT")]
        public long solarSystemID { get; set; }
    }
}
