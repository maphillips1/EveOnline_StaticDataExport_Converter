using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes
{
    internal class BlueprintActivityMaterial
    {
        public BlueprintActivityMaterial() {
            activityName = "";
        }
        [Attributes.SQLiteType("INT")]
        public int blueprintTypeID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int quantity {  get; set; }

        [Attributes.SQLiteType("INT")]
        public int typeId { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string activityName { get; set; }

    }
}
