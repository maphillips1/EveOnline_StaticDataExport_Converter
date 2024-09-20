using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.FSD.Supporting_Classes.Converters
{
    internal class CertificateSkillTypeConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            List<CertificateSkillType> certificateSkillTypes = new List<CertificateSkillType>();
            CertificateSkillType newCertificateSkillType;
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);

            foreach (JToken jToken in jObject.Children())
            {
                newCertificateSkillType = Newtonsoft.Json.JsonConvert.DeserializeObject<CertificateSkillType>(jToken.First.ToString());
                if (newCertificateSkillType != null)
                {
                    newCertificateSkillType.skillTypeID = Convert.ToInt32(jToken.Path);
                    certificateSkillTypes.Add(newCertificateSkillType);
                }
            }

            return certificateSkillTypes;
        }
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
