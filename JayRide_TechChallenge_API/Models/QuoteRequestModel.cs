namespace JayRide_TechChallenge_API.Models
{


    public class QuoteRequestModel
    {
        public string from { get; set; }
        public string to { get; set; }
        public Listing[] listings { get; set; }
    }

    public class Listing
    {
        public string name { get; set; }
        public float pricePerPassenger { get; set; }
        public Vehicletype vehicleType { get; set; }
        public float totalPrice { get; set; }
        
    }

    public class Vehicletype
    {
        public string name { get; set; }
        public int maxPassengers { get; set; }
    }
}

