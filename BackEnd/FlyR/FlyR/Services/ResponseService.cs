using FlyR.Interfaces;
using FlyR.Models;

namespace FlyR.Services
{
    public class ResponseService : IResponse
    {
        private readonly IConfiguration _configuration;
        public ResponseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<dynamic> GetAllAsync()
        {
            string page = _configuration["url"];
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Fiddler");
                using (HttpResponseMessage response = await client.GetAsync(page))
                using (HttpContent content = response.Content)
                {
                    string result = await content.ReadAsStringAsync();

                    return result;

                }

            }
        }


    }
}
