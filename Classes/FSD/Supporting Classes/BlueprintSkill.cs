using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes
{
    internal class BlueprintSkill
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int parentTypeId {  get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string activityName { get; set; }
        [Attributes.SQLiteType("INT")]
        public int level { get; set; }
        [Attributes.SQLiteType("INT")]
        public int typeID { get; set; }
    }
}
