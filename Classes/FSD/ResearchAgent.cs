﻿using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
{
    internal class ResearchAgent
    {
        [Attributes.SQLiteType("INT")]
        public int researchAgentID {  get; set; }

        [Attributes.SQLiteType("INT")]
        public int skillTypeID { get; set; }

        [Attributes.SQLIgnore()]
        public List<ResearchAgentSkillType> skills { get; set; }

    }
}