using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.Universe.Supporting_Classes.Converters
{
    internal class PlanetAsteroidBeltConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            List<mapAsteroidBelt> planetAsteroidBelts = new List<mapAsteroidBelt>();
            mapAsteroidBelt newPlanetAsteroidBelt = null;

            JObject jObject = JObject.Load(reader);

            foreach (JToken jToken in jObject.Children())
            {
                newPlanetAsteroidBelt = Newtonsoft.Json.JsonConvert.DeserializeObject<mapAsteroidBelt>(jToken.First.ToString());
                if (newPlanetAsteroidBelt != null)
                {
                    newPlanetAsteroidBelt.asteroidBeltID = Convert.ToInt32(jToken.Path);

                    planetAsteroidBelts.Add(newPlanetAsteroidBelt);
                }
            }

            return planetAsteroidBelts;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
