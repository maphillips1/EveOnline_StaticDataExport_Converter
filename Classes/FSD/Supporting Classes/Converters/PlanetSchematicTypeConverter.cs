using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes.Converters
{
    internal class PlanetSchematicTypeConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            List<PlanetSchematicType> planetSchematicTypes = new List<PlanetSchematicType>();
            PlanetSchematicType newPlanetSchematicType;
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);

            foreach (JToken jToken in jObject.Children())
            {
                newPlanetSchematicType = Newtonsoft.Json.JsonConvert.DeserializeObject<PlanetSchematicType>(jToken.First.ToString());
                if (newPlanetSchematicType != null)
                {
                    newPlanetSchematicType.planetSchematicTypeID = Convert.ToInt32(jToken.Path);
                    planetSchematicTypes.Add(newPlanetSchematicType);
                }
            }

            return planetSchematicTypes;
        }
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
