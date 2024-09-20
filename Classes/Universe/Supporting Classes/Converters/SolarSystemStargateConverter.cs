using EveStaticDataExportConverter.Classes.FSD;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveStaticDataExportConverter.Classes.Universe.SupportingClasses;

namespace EveStaticDataExportConverter.Classes.Universe.Supporting_Classes.Converters
{
    internal class SolarSystemStargateConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            List<SolarSystemStargate> solarSystemStargates = new List<SolarSystemStargate>();
            SolarSystemStargate newSolarSystemStargate = null;

            JObject jObject = JObject.Load(reader);

            foreach (JToken jToken in jObject.Children())
            {
                newSolarSystemStargate = Newtonsoft.Json.JsonConvert.DeserializeObject<SolarSystemStargate>(jToken.First.ToString());
                if (newSolarSystemStargate != null)
                {
                    newSolarSystemStargate.stargateID = Convert.ToInt32(jToken.Path);

                    if (newSolarSystemStargate.position?.Count > 0)
                    {
                        newSolarSystemStargate.positionX = newSolarSystemStargate.position[0];
                        newSolarSystemStargate.positionY = newSolarSystemStargate.position[1];
                        newSolarSystemStargate.positionZ = newSolarSystemStargate.position[2];
                    }

                    solarSystemStargates.Add(newSolarSystemStargate);
                }
            }

            return solarSystemStargates;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
