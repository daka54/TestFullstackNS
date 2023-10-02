using FlyR.Models;

namespace FlyR.Interfaces
{
    public interface IFlightManagement
    {
        JourneyDto CalculateJourney(string origin, string destination);
    }
}
