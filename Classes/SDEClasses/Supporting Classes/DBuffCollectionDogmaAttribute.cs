using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes
{
    internal class DBuffCollectionDogmaAttribute
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int dbuffCollectionId {  get; set; }

        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public int dogmaAttributeID {  get; set; }
    }
}
