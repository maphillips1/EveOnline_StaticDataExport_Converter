using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
{
    internal class StationService
    {
        [Attributes.SQLiteType("INT")]
        public int stationServiceID {  get; set; }
        [Attributes.SQLIgnore()]
        public LanguageDescription serviceNameID { get; set; }
    }
}
