using JayRide_TechChallenge_API.Models;
using JayRide_TechChallenge_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JayRide_TechChallenge_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {

        private readonly ICandidateService _candidateService;
        private readonly ILocationByIPService _locationByIPService;
        private readonly IQuoteRequestService _quoteRequestService;

        #region Constructor
        public CandidatesController(ICandidateService candidateService,
                                    ILocationByIPService locationByIPService,
                                    IQuoteRequestService quoteRequestService)
        {
            _candidateService = candidateService;
            _locationByIPService = locationByIPService;
            _quoteRequestService = quoteRequestService;
        }
        #endregion

        [HttpGet]
        [Route("/Candidate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CandidateModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult getCandidate()
        {
            var candidate = _candidateService.getCandidate();
            return candidate == null ? NotFound() : Ok(candidate);
        }

        [HttpGet]
        [Route("/Location")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task< IActionResult> getLocation()
        {
            string ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            if(ip=="::1")
            {
                ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[2].ToString();
            }
            

            if( string.IsNullOrEmpty(ip)) return NotFound();
            var location = await _locationByIPService.LocationByIp(ip.ToString());
            return location == null ? NotFound() : Ok(location);

        }
        [HttpGet]
        [Route("/Listings")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuoteRequestModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> getQuoteRequests()
        {
            var listings = await _quoteRequestService.GetQuoteRequests();
            return listings == null ? NotFound() : Ok(listings);
        }

    }
}
