using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD
{
    public class AgentInSpace
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public long agentInSpaceId { get; set; }

        [Attributes.SQLiteType("INT")]
        public int dungeonID { get; set; }

        [Attributes.SQLiteType("INT")]
        public long solarSystemID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int spawnPointID { get; set; }

        [Attributes.SQLiteType("INT")]
        public int typeID { get; set; }
    }
}
