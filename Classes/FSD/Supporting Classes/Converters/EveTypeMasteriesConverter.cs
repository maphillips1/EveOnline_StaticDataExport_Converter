using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes.Converters
{
    internal class EveTypeMasteriesConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            List<EveTypeMastery> eveTypeMasteries = new List<EveTypeMastery>();
            EveTypeMastery newEveTypeMastery = null;
            List<int> rawMasteryTypes;

            JObject jObject = JObject.Load(reader);

            foreach(JToken token in jObject.Children())
            {
                foreach (Object o in token.Values().ToList())
                {
                    newEveTypeMastery = new EveTypeMastery();
                    newEveTypeMastery.masteryID = Convert.ToInt32(token.Path);
                    newEveTypeMastery.masteryTypeId = Convert.ToInt32(o);
                    eveTypeMasteries.Add(newEveTypeMastery);
                }

            }

            return eveTypeMasteries;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
