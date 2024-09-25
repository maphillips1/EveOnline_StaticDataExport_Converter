using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
{
    internal class SkinLicense
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int skinLicenseID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public int duration { get; set; }
        [Attributes.SQLiteType("INT")]
        public bool isSingleUse { get; set; }
        [Attributes.SQLiteType("INT")]
        public int licenseTypeID { get; set; }
        [Attributes.SQLiteType("INT")]
        public int skinID { get; set; }
    }
}
