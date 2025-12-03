using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes
{
    public class ReleaseInformation
    {
        [Attributes.SQLiteType("TEXT")]
        public string _key { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string buildNumber { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string releaseDate { get; set; }
    }
}
