using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes
{
    internal class BlueprintActivities
    {

        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int blueprintTypeID {  get; set; }

        [Attributes.SQLIgnore()]
        public BlueprintActivityType copying {  get; set; }

        [Attributes.SQLIgnore()]
        public BlueprintActivityType manufacturing { get; set; }

        [Attributes.SQLIgnore()]
        public BlueprintActivityType research_material { get; set; }

        [Attributes.SQLIgnore()]
        public BlueprintActivityType research_time { get; set; }

        [Attributes.SQLIgnore()]
        public BlueprintActivityType reaction { get; set; }

        [Attributes.SQLIgnore()]
        public BlueprintActivityType invention { get; set; }

        [Attributes.SQLiteType("INT")]
        public int maxProductionLimit { get; set; }
    }
}
