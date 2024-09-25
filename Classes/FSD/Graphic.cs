using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
{
    internal class Graphic
    {
        public Graphic() 
        {
            this.description = "";
            this.sofFactionName = "";
            this.sofHullName = "";
            this.sofRaceName = "";
            this.iconFolder = "";
        }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int graphicID {  get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string description { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string sofFactionName { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string sofHullName { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string sofRaceName { get; set; }

        [Attributes.SQLIgnore()]
        public IconInfo iconInfo { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string iconFolder { get; set; }

    }
}
