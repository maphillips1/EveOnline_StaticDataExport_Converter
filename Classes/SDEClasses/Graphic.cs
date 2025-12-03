using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses
{
    internal class Graphic
    {
        public Graphic() 
        {
            sofFactionName = "";
            sofHullName = "";
            sofRaceName = "";
            iconFolder = "";
            graphicFile = "";
        }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        [Newtonsoft.Json.JsonProperty("_key")]
        public int graphicID {  get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string graphicFile { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string sofFactionName { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string sofHullName { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string sofRaceName { get; set; }

        [Attributes.SQLiteType("TEXT")]
        public string iconFolder { get; set; }

    }
}
