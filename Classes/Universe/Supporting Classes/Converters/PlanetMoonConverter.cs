using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.Universe.Supporting_Classes.Converters
{
    internal class PlanetMoonConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            List<mapMoon> planetMoons = new List<mapMoon>();
            mapMoon newPlanetMoon = null;

            JObject jObject = JObject.Load(reader);

            foreach (JToken jToken in jObject.Children())
            {
                newPlanetMoon = Newtonsoft.Json.JsonConvert.DeserializeObject<mapMoon>(jToken.First.ToString());
                if (newPlanetMoon != null)
                {
                    newPlanetMoon.moonID = Convert.ToInt32(jToken.Path);

                    planetMoons.Add(newPlanetMoon);
                }
            }

            return planetMoons;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
