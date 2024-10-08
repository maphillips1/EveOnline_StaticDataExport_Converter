﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes
{
    internal class CorporationTrade
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public long npcCorporationID { get; set; }
        [Attributes.SQLiteType("INT")]
        public int corporationTradeID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public double corporationTradeAmount { get; set; }  
    }
}
