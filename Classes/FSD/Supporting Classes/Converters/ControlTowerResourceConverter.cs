using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes.Converters
{
    internal class ControlTowerResourceConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            List<ControlTowerResource> controlTowerResources = new List<ControlTowerResource>();
            ControlTowerResource newControlTowerResource = null;

            JArray jArray = JArray.Load(reader);

            foreach (JObject jObject in jArray)
            {
                newControlTowerResource = Newtonsoft.Json.JsonConvert.DeserializeObject<ControlTowerResource>(jObject.ToString());
                if (newControlTowerResource != null)
                {
                    controlTowerResources.Add(newControlTowerResource);
                }
            }

            return controlTowerResources;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
