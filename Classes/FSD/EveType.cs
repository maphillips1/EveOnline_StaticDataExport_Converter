using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes;
using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
{
    internal class EveType
    {
        public EveType()
        {
            sofFactionName = "";
        }
        [Attributes.SQLiteType("INT")]
        public int typeID {  get; set; }

        [Attributes.SQLiteType("INT")]
        public double basePrice { get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal capacity {  get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription description { get; set; }

        [Attributes.SQLiteType("INT")]
        public long factionID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int graphicID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int groupID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int marketGroupID { get; set; }

        [Attributes.SQLiteType("INT")]
        public double mass { get; set; }

        [Attributes.SQLIgnore()]
        [Newtonsoft.Json.JsonConverter(typeof(EveTypeMasteriesConverter))]
        public List<EveTypeMastery> masteries { get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal metaGroupID { get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription name { get; set; }

        [Attributes.SQLiteType("INT")]
        public int portionSize { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool published { get; set; }

        [Attributes.SQLiteType("INT")]
        public int raceID { get; set; }

        [Attributes.SQLiteType("INT")]
        public decimal radius { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string sofFactionName { get; set; }

        [Attributes.SQLiteType("INT")]
        public int soundID { get; set; }

        [Attributes.SQLIgnore()]
        public EveTypeTrait traits { get; set; }

        [Attributes.SQLiteType("INT")]
        public double volume { get; set; }
    }
}
