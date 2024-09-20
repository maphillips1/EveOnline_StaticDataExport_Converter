using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.Database
{
    public class TableInfo
    {
        public string Name { get; set; }
        public List<string> columns { get; set; }
    }
}
