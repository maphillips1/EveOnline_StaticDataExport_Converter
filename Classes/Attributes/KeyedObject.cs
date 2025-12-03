using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.Attributes
{
    interface KeyedObject
    {
        long GetKey1();
        long GetKey2();
        string GetCategory();
    }
}
