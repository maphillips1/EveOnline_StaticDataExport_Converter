using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
{
    internal class Faction
    {
        [Attributes.SQLiteType("INT")]
        public long factionID { get; set; }

        [Attributes.SQLiteType("INT")]
        public long corporationID { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription descriptionID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int iconID { get; set; }

        [Attributes.SQLIgnore()]
        public List<int> memberRaces { get; set; }

        [Attributes.SQLiteType("INT")]
        public long militiaCorporationID { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription nameID { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription shortDescriptionID { get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal sizeFactor { get; set; }

        [Attributes.SQLiteType("INT")]
        public long solarSystemID { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool uniqueName { get; set; }

    }
}
