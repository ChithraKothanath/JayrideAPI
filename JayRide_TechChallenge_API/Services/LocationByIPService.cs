using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.Primitives;
using System.Net;
using Microsoft.AspNetCore.Http.Features;

namespace JayRide_TechChallenge_API.Services
{
    public class LocationByIPService:ILocationByIPService
    {
        private HttpClient _httpClient;
        private IHttpContextAccessor _accessor;

        public LocationByIPService(IHttpClientFactory httpClientFactory, IHttpContextAccessor accessor)
        {
            this._httpClient = httpClientFactory.CreateClient();
            this._httpClient.BaseAddress = new Uri("http://api.ipstack.com/");
            this._accessor = accessor;

        }
        public async Task<string> LocationByIp(string ip)
        {
            var response = await this._httpClient.GetAsync(ip + "?access_key=cf88039a4314bb0a229a1fedd5de92e2");
            response.EnsureSuccessStatusCode();
            var stringResult = await response.Content.ReadAsStringAsync();
            var jobject = (JObject)JsonConvert.DeserializeObject(stringResult);
            string City = (string)jobject["city"];
            string Country = (string)jobject["region_name"];
            string CountryCode = (string)jobject["country_code"];

            return City ==null? Country:City;
        }
    }
}
