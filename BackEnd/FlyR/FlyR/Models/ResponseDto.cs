namespace FlyR.Models
{
    public class ResponseDto
    {
        public string departureStation { get; set; }
        public string arrivalStation { get; set; }
        public string flightCarrier { get; set; }
        public string flightNumber { get; set; }
        public double price { get; set; }

        public ResponseDto copy()
        {
            return new ResponseDto {departureStation = this.departureStation, arrivalStation= this.arrivalStation,flightCarrier= this.flightCarrier,flightNumber = this.flightNumber,price= this.price}; 
        }
    }


}
