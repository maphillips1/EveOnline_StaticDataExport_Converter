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
        [Attributes.SQLiteIndex()]
        public long solarSystemID {  get; set; }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public long stargateID { get; set; }
    }
}
