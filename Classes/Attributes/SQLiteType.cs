using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.Attributes
{
    public class SQLiteType : System.Attribute
    {
        public string name;

        public SQLiteType(string name)
        {
            this.name = name;
        }
    }
}
