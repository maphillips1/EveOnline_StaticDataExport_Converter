using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes.Converters
{
    internal class NPCCorpDivisionConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            List<NPCCorporationCorpDivision> npcCorpDivisions = new List<NPCCorporationCorpDivision>();
            NPCCorporationCorpDivision newNPCCorpDivion = null;

            JObject jObject = JObject.Load(reader);

            foreach (JToken jToken in jObject.Children())
            {
                newNPCCorpDivion = Newtonsoft.Json.JsonConvert.DeserializeObject<NPCCorporationCorpDivision>(jToken.First.ToString());
                if (newNPCCorpDivion != null)
                {
                    newNPCCorpDivion.npcCorporationDivisionID = Convert.ToInt32(jToken.Path);
                    npcCorpDivisions.Add(newNPCCorpDivion);
                }
            }

            return npcCorpDivisions;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
