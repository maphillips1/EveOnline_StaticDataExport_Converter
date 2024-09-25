using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes
{
    internal class BlueprintProduct
    {
        public BlueprintProduct() {
            this.activityName = "";
        }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int blueprintTypeID {  get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string activityName { get; set; }

        [Attributes.SQLiteType("INT")]
        public int typeID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int quantity { get; set; }
        [Attributes.SQLiteType("INT")]
        public decimal probability { get; set; }
    }
}
