using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
{
    internal class DogmaAttributeCategory
    {
        public DogmaAttributeCategory() 
        {
            this.description = "";
            this.name = "";
        }
        [Attributes.SQLiteType("INT")]
        public int dogmaAttributeCategoryID { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string description { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string name { get; set; }
    }
}
