﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes
{
    internal class FactionRaces
    {
        [Attributes.SQLiteType("INT")]
        public long factionID {  get; set; }

        [Attributes.SQLiteType("INT")]
        public int raceID { get; set; }
    }
}
