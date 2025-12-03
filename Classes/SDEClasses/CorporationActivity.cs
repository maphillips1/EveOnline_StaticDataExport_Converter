using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class CorporationActivity
    {
        public CorporationActivity() {
            nameID = new LanguageDescription();
        }

        [Attributes.SQLIgnore()]
        [Attributes.SQLiteIndex()]
        public int corporationActivityID { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription nameID { get; set; }
    }
}
