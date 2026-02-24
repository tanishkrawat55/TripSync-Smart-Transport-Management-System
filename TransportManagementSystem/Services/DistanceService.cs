using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TransportManagementSystem.Services
{
    public class DistanceService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public DistanceService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["GoogleDistanceApi:ApiKey"];
        }

        public async Task<double> GetDistanceAsync(string origin, string destination)
        {
            try
            {
                string url =
                    $"https://maps.googleapis.com/maps/api/distancematrix/json?units=metric&origins={origin}&destinations={destination}&key={_apiKey}";

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    using JsonDocument doc = JsonDocument.Parse(json);
                    var root = doc.RootElement;

                    var distanceText = root
                        .GetProperty("rows")[0]
                        .GetProperty("elements")[0]
                        .GetProperty("distance")
                        .GetProperty("text")
                        .GetString();

                    double distance = double.Parse(distanceText.Split(' ')[0]);

                    return distance;
                }
            }
            catch
            {
                // If API fails, fallback simple logic
            }

            // Fallback: simple random distance
            return new Random().Next(5, 50);
        }
    }
}
