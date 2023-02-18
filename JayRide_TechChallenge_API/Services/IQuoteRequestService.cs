using JayRide_TechChallenge_API.Models;

namespace JayRide_TechChallenge_API.Services
{
    public interface IQuoteRequestService
    {
        public Task<QuoteRequestModel> GetQuoteRequests();
    }
}
