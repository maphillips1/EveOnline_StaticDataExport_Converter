using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes.Converters
{
    internal class NPCCorporationTradeConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            List<CorporationTrade> corporationTrades = new List<CorporationTrade>();
            CorporationTrade newCorporationTrade;
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);

            foreach (JToken jToken in jObject.Children())
            {
                newCorporationTrade = new CorporationTrade();
                newCorporationTrade.corporationTradeID = Convert.ToInt32(jToken.Path);
                newCorporationTrade.corporationTradeAmount = Convert.ToDouble(jToken.Values().First());
                corporationTrades.Add(newCorporationTrade);
            }

            return corporationTrades;
        }
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
