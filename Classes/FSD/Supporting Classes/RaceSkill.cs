using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes
{
    internal class RaceSkill
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int raceID {  get; set; }

        [Attributes.SQLiteType("INT")]
        public int skillTypeId { get; 
            set; }
        [Attributes.SQLiteType("INT")]
        public int skillLevel { get; set; }
    }
}
