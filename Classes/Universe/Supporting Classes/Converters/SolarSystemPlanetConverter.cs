using EveStaticDataExportConverter.Classes.Universe.SupportingClasses;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.Universe.Supporting_Classes.Converters
{
    internal class SolarSystemPlanetConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            List<mapPlanet> solarSystemPlanets = new List<mapPlanet>();
            mapPlanet newSolarSystemPlanet = null;

            JObject jObject = JObject.Load(reader);

            foreach (JToken jToken in jObject.Children())
            {
                newSolarSystemPlanet = Newtonsoft.Json.JsonConvert.DeserializeObject<mapPlanet>(jToken.First.ToString());
                if (newSolarSystemPlanet != null)
                {
                    newSolarSystemPlanet.planetID = Convert.ToInt32(jToken.Path);


                    solarSystemPlanets.Add(newSolarSystemPlanet);
                }
            }

            return solarSystemPlanets;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
