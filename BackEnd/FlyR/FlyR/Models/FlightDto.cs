namespace FlyR.Models
{
    public class FlightDto
    {
        public String Origin { get; set; }
        public String Destination { get; set; }
        public double Price { get; set; }
        public TransportDto Transport { get; set; }
    }
}
