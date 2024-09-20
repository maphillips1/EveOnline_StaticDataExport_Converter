using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.Universe.Supporting_Classes
{
    internal class StarStatistics
    {
        public StarStatistics()
        {
            spectralClass = "";
        }
        [Attributes.SQLiteType("INT")]
        public long starID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public double age { get; set; }
        [Attributes.SQLiteType("INT")]
        public double life {  get; set; }
        [Attributes.SQLiteType("INT")]
        public bool locked { get; set; }
        [Attributes.SQLiteType("INT")]
        public decimal luminosity { get; set; }
        [Attributes.SQLiteType("INT")]
        public double radius { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string spectralClass { get; set; }
        [Attributes.SQLiteType("INT")]
        public decimal temperature { get; set; }
    }
}
