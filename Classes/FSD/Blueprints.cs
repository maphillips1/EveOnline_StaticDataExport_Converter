using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
{
    internal class Blueprints
    {
        [Attributes.SQLIgnore()]
        public BlueprintActivities activities {  get; set; }

        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int blueprintTypeID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int maxProductionLimit { get; set; }
    }
}
