using RestSharp;
using System.Net;
using System.Threading.Tasks;
using WeatherSample.Models;

namespace WeatherSample.Services
{
    public class ForecastProviderService
    {
        private readonly RestClient _client = new RestClient("https://localhost:5001/api/forecast");

        public async Task<ForecastModel.City?> ForecastOf(string city)
        {
            var response = await _client.ExecuteGetAsync<ForecastModel.City>(new RestRequest($"/{city}"));
            return response.StatusCode == HttpStatusCode.NotFound ? null : response.Data;
        }
    }
}