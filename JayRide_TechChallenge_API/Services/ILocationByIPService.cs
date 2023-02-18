namespace JayRide_TechChallenge_API.Services
{
    public interface ILocationByIPService
    {
        public Task<string> LocationByIp(string ip);
    }
}
