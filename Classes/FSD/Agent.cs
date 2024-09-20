using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
{
    public class Agent
    {

        [Attributes.SQLiteType("INT")]
        public long agentId { get; set; }

        [Attributes.SQLiteType("INT")]
        public int agentTypeID { get; set; }

        [Attributes.SQLiteType("INT")]
        public long corporationID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int divisionID { get; set; }

        [Attributes.SQLiteType("INT")]
        public bool isLocator { get; set; }

        [Attributes.SQLiteType("INT")]
        public int level { get; set; }

        [Attributes.SQLiteType("INT")]
        public long locationID { get; set; }
    }
}
