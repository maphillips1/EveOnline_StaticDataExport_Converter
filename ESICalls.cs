using EveStaticDataExportConverter.Classes.Universe.Supporting_Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter
{
    internal static class ESICalls
    {
        private static string solarSystemSovURL = "https://esi.evetech.net/latest/sovereignty/map/?datasource=tranquility";

        public static List<SolarSystemSovereignty> GetSolarSystemSovereignties()
        {
            List<SolarSystemSovereignty> sovereignties = new List<SolarSystemSovereignty>();

            string uri = solarSystemSovURL;
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            System.Net.Http.HttpResponseMessage response = null;
            int retryCount = 0;
            while (retryCount < 10)
            {
                response = client.GetAsync(uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    break;
                }
            }

            if (response != null && response.IsSuccessStatusCode)
            {
                string sovJson = response.Content.ReadAsStringAsync().Result;
                sovereignties = JsonConvert.DeserializeObject<List<SolarSystemSovereignty>>(sovJson);
            }
            return sovereignties;
        }
    }
}
