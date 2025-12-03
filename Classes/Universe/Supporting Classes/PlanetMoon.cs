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
        public long planetID { get; set; }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public long moonID { get; set; }
    }
}
