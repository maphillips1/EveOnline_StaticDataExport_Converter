using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
{
    internal class Category
    {
        public Category() 
        {
            this.name = new LanguageDescription();
        }
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int categoryID {  get; set; }

        [Attributes.SQLIgnore()]
        public LanguageDescription name { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool published { get; set; }
    }
}
