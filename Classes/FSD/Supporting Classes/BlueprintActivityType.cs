using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes
{
    internal class BlueprintActivityType
    {
        public BlueprintActivityType() {
            activityName = "";
            materials = new List<BlueprintActivityMaterial>();
            skills = new List<BlueprintSkill>();
        }

        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int blueprintTypeID { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string activityName { get; set; }

        [Attributes.SQLiteType("INT")]
        public int time { get; set; }

        [Attributes.SQLIgnore()]
        public List<BlueprintActivityMaterial> materials { get; set; }

        [Attributes.SQLIgnore()]
        public List<BlueprintSkill> skills { get; set; }

        [Attributes.SQLIgnore() ]
        public List<BlueprintProduct> products { get; set; }

    }
}
