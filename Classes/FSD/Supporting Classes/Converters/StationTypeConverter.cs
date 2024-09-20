using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes.Converters
{
    internal class StationTypeConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            List<StationType> stationTypes = new List<StationType>();
            StationType stationType;
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);

            foreach (JToken jToken in jObject.Children())
            {
                stationType = new StationType();
                stationType.stationTypeID = Convert.ToInt32(jToken.Path);
                stationType.typeID = Convert.ToInt32(jToken.Values().First());
                stationTypes.Add(stationType);
            }

            return stationTypes;
        }
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
