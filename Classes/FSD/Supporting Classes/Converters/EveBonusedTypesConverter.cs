using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes.Converters
{
    internal class EveBonusedTypesConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            List<EveTypeBonus> bonusedEveTypes = new List<EveTypeBonus>();

            JObject jObject = JObject.Load(reader);

            foreach (JToken jToken in jObject.Children())
            {
                bonusedEveTypes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EveTypeBonus>>(jToken.First.ToString());
                if (bonusedEveTypes != null)
                {
                    foreach (EveTypeBonus eveTypeBonus in bonusedEveTypes)
                    {
                        eveTypeBonus.bonusedTypeID = Convert.ToInt32(jToken.Path);
                    }
                }
            }

            return bonusedEveTypes;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
