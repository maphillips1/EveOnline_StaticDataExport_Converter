using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes
{
    internal class CertificateRecommendedForType
    {
        [Attributes.SQLiteType("INT")]
        public int certificateID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public int recommendedForTypeID { get; set; }
    }
}
