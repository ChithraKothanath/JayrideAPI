using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.Primitives;
using System.Net;
using Microsoft.AspNetCore.Http.Features;
using JayRide_TechChallenge_API.Models;
using System.Linq;
namespace JayRide_TechChallenge_API.Services
{
    public class QuoteRequestService: IQuoteRequestService
    {
        private HttpClient _httpClient;
        public QuoteRequestService(IHttpClientFactory httpClientFactory)
        {
            this._httpClient = httpClientFactory.CreateClient();
            this._httpClient.BaseAddress = new Uri("https://jayridechallengeapi.azurewebsites.net/api/");
        }

        public async Task<QuoteRequestModel> GetQuoteRequests()
        {
            var response2 = await this._httpClient.GetAsync("QuoteRequest");
            response2.EnsureSuccessStatusCode();
            var stringResult = await response2.Content.ReadAsStringAsync();
            var jobject = JsonConvert.DeserializeObject<QuoteRequestModel>(stringResult);

            if(jobject!=null)
            {
                
                //Utilising the listings array, filter out listings that don’t support the number of passengers.
                //could not understand logic for this condition

            Listing[] listings = jobject.listings

                                                .GroupBy(x => new { x.name, x.pricePerPassenger,x.vehicleType })
                                                .Select(g => new Listing
                                                        {
                                                            name = g.Key.name,
                                                            pricePerPassenger = g.Key.pricePerPassenger,
                                                            vehicleType=g.Key.vehicleType,
                                                            totalPrice=g.Key.pricePerPassenger * g.Key.vehicleType.maxPassengers
                                                        })
                                                .OrderByDescending(g => g.totalPrice).ToArray();
                jobject.listings = listings;

            }

            
            return jobject;
        }

    }
}
