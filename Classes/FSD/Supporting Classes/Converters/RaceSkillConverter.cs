using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes.Converters
{
    internal class RaceSkillConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            List<RaceSkill> raceSkills = new List<RaceSkill>();
            RaceSkill newRaceSkill;
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);

            foreach (JToken jToken in jObject.Children())
            {
                newRaceSkill = new RaceSkill();
                newRaceSkill.skillTypeId = Convert.ToInt32(jToken.Path);
                newRaceSkill.skillLevel = Convert.ToInt32(jToken.Values().First());
                raceSkills.Add(newRaceSkill);

            }

            return raceSkills;
        }
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
