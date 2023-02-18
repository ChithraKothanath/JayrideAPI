using JayRide_TechChallenge_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace JayRide_TechChallenge_API.Services
{
    public class CandidateService : ICandidateService
    {       
        public CandidateModel getCandidate()
        {
            return   new CandidateModel("Test","Test");
        }
    }
}
