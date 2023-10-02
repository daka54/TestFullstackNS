using FlyR.Interfaces;
using FlyR.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace FlyR.Services
{
    /// <summary>
    /// A service class for managing flight-related operations, including finding the cheapest route between two cities.
    /// </summary>
    public class FlightManagementService : IFlightManagement
    {
        private IResponse _response;

        /// <summary>
        /// Initializes a new instance of the FlightManagementService class.
        /// </summary>
        /// <param name="Response">An implementation of the IResponse interface for data retrieval.</param>
        public FlightManagementService(IResponse Response)
        {
            this._response = Response;
        }

        /// <summary>
        /// Retrieves all flight-related data and calculates the cheapest journey between two cities.
        /// </summary>
        /// <param name="origin">The origin city.</param>
        /// <param name="destination">The destination city.</param>
        /// <returns>A JourneyDto object representing the cheapest journey or null if no valid journey is found.</returns>
        public JourneyDto CalculateJourney(string origin, string destination)
        {
            var responseList = GetAll();
            var route = FindCheapestRoute(origin, destination, responseList);
            if (route != null)
            {
                return BuildJourney(route, origin, destination);
            }
            return null;
        }

        /// <summary>
        /// Retrieves all flight-related data from the IResponse implementation.
        /// </summary>
        /// <returns>A list of ResponseDto objects representing flight information.</returns>
        private List<ResponseDto> GetAll()
        {
            var data = _response.GetAllAsync().Result;
            List<ResponseDto> responseList = JsonConvert.DeserializeObject<List<ResponseDto>>(data);
            return responseList;
        }

        /// <summary>
        /// Finds the cheapest flight route between two cities using Dijkstra's algorithm.
        /// </summary>
        /// <param name="origin">The origin city.</param>
        /// <param name="destination">The destination city.</param>
        /// <param name="routes">A list of ResponseDto objects representing flight routes.</param>
        /// <returns>A list of ResponseDto , or null if no valid route is found.</returns>
        private List<ResponseDto> FindCheapestRoute(string origin, string destination, List<ResponseDto> routes)
        {
            Dictionary<string, double> costSoFar = new Dictionary<string, double>();
            costSoFar[origin] = 0;

            Dictionary<string, List<ResponseDto>> currentRoute = new Dictionary<string, List<ResponseDto>>();
            currentRoute[origin] = new List<ResponseDto>();

            SortedSet<Tuple<double, string>> priorityQueue = new SortedSet<Tuple<double, string>>();
            priorityQueue.Add(new Tuple<double, string>(0, origin));

            while (priorityQueue.Count > 0)
            {
                var (currentCost, currentCity) = priorityQueue.First();
                priorityQueue.Remove(priorityQueue.First());

                if (currentCity == destination)
                {
                    return currentRoute[destination];
                }
                var possibleRoutes = routes.Where(r => r.departureStation == currentCity);

                foreach (var route in possibleRoutes)
                {
                    string nextCity = route.arrivalStation;
                    double newCost = costSoFar[currentCity] + route.price;

                    if (!costSoFar.ContainsKey(nextCity) || newCost < costSoFar[nextCity])
                    {
                        costSoFar[nextCity] = newCost;
                        currentRoute[nextCity] = new List<ResponseDto>(currentRoute[currentCity]);
                        currentRoute[nextCity].Add(route);
                        priorityQueue.Add(new Tuple<double, string>(newCost, nextCity));
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Builds a JourneyDto object representing the journey based on a list of flight routes.
        /// </summary>
        /// <param name="routes">A list of ResponseDto objects representing flight routes.</param>
        /// <param name="origin">The origin city.</param>
        /// <param name="destination">The destination city.</param>
        /// <returns>A JourneyDto object representing the journey with details such as origin, destination, price, and individual flights.</returns>
        private JourneyDto BuildJourney(List<ResponseDto> routes, string origin, string destination)
        {
            var journey = new JourneyDto
            {
                Origin = origin,
                Destination = destination,
                Price = routes.Sum(route => route.price),
                Flights = routes.Select(route => new FlightDto
                {
                    Origin = route.departureStation,
                    Destination = route.arrivalStation,
                    Price = route.price,
                    Transport = new TransportDto
                    {
                        FlightCarrier = route.flightCarrier,
                        FlightNumber = route.flightNumber
                    }
                }).ToList()
            };

            return journey;
        }
    }
}
