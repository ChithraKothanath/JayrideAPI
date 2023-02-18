namespace JayRide_TechChallenge_API.Models
{
    public class CandidateModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }

        public CandidateModel(string name,string phone)
        {
            Name = name;
            Phone = phone;
        }

    }
}
