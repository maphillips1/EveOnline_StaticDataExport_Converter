using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes
{
    internal class EveTypeTrait
    {
        public int typeID {  get; set; }
        public List<EveTypeBonus> miscBonuses { get; set; }
        public List<EveTypeBonus> roleBonuses { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(EveBonusedTypesConverter))]
        public List<EveTypeBonus> types { get; set; }
    }
}
