using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using XamarinApplication.Core.Models;

namespace XamarinApplication.Core
{
    public class WeatherClient : IWeatherClient
    {
        private string result = null;

        private string text = "";
        private string icon = "";
        private double? temperature = 0;

        private readonly HttpClient httpClient;

        public WeatherClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Response> SearchAsync(string city)
        {
            Location location;
            Information information;

            string url = $"&q={city}&aqi=no";

            try
            {
                result = await httpClient.GetStringAsync(url);
                text = "";
            }
            catch (HttpRequestException)
            {
                text = "Inserisci una città valida";
            }
            catch (InvalidOperationException)
            {
                text = "Error";
            }

            if (!string.IsNullOrEmpty(text))
            {
                return null;
            }

            JObject deserialized = JObject.Parse(result);
            try
            {
                temperature = deserialized["current"]?["temp_c"]?.ToObject<double?>();
                text = deserialized["current"]?["condition"]?["text"]?.ToString();
                icon = deserialized["current"]?["condition"]?["icon"]?.ToString();
                location = deserialized["location"]?.ToObject<Location>();
                information = new Information(text, icon, temperature);
            }
            catch (NullReferenceException)
            {
                location = null;
                information = null;
            }
            catch (InvalidOperationException)
            {
                location = null;
                information = null;
            }

            if (location == null && information == null)
            {
                return null;
            }
            else
            {
                return new Response(information, location);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing && httpClient != null)
            {
                httpClient.Dispose();
            }
        }
    }
}