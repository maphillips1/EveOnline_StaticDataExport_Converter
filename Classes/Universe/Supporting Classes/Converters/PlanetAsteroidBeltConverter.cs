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
            List<PlanetAsteroidBelt> planetAsteroidBelts = new List<PlanetAsteroidBelt>();
            PlanetAsteroidBelt newPlanetAsteroidBelt = null;

            JObject jObject = JObject.Load(reader);

            foreach (JToken jToken in jObject.Children())
            {
                newPlanetAsteroidBelt = Newtonsoft.Json.JsonConvert.DeserializeObject<PlanetAsteroidBelt>(jToken.First.ToString());
                if (newPlanetAsteroidBelt != null)
                {
                    newPlanetAsteroidBelt.asteroidBeltID = Convert.ToInt32(jToken.Path);

                    if (newPlanetAsteroidBelt.position?.Count > 0)
                    {
                        newPlanetAsteroidBelt.positionX = newPlanetAsteroidBelt.position[0];
                        newPlanetAsteroidBelt.positionY = newPlanetAsteroidBelt.position[1];
                        newPlanetAsteroidBelt.positionZ = newPlanetAsteroidBelt.position[2];
                    }

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
