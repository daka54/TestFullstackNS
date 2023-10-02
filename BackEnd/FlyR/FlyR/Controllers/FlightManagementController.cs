using FlyR.Interfaces;
using FlyR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FlyR.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("FlightManagement/[action]")]
    public class FlightManagementController : ControllerBase
    {
        private IFlightManagement _flightmanagement;

        public FlightManagementController(IFlightManagement FlightManagement)
        {
            this._flightmanagement = FlightManagement;
        }


        [HttpGet]
        public HttpMessage<JourneyDto> CalculateJourney(string origin, string destination)
        {
            var data = _flightmanagement.CalculateJourney(origin, destination);

            return new HttpMessage<JourneyDto>
            {
                StatusCode = (data != null) ? HttpStatusCode.OK : HttpStatusCode.NoContent,
                IsSuccess = true,
                Data = data
            };
        }
    }
}