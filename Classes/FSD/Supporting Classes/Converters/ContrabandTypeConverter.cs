using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes.Converters
{
    internal class ContrabandTypeConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            List<ContrabandType> contrabandTypes = new List<ContrabandType>();
            ContrabandType newContrabandType = null;

            JObject jObject = JObject.Load(reader);

            foreach (JToken jToken in jObject.Children())
            {
                newContrabandType = Newtonsoft.Json.JsonConvert.DeserializeObject<ContrabandType>(jToken.First.ToString());
                if (newContrabandType != null)
                {
                    newContrabandType.factionID = Convert.ToInt32(jToken.Path);
                    contrabandTypes.Add(newContrabandType);
                }
            }

            return contrabandTypes;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
