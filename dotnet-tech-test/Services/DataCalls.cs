using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using dotnet_tech_test.Interfaces;
using Microsoft.Extensions.Logging;

namespace dotnet_tech_test.Services
{
    public class DataCalls : IDataCalls
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<DataCalls> _logger;

        public DataCalls(IHttpClientFactory httpClientFactory, ILogger<DataCalls> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public virtual async Task<T?> GetData<T>(string endpoint)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetStringAsync(endpoint);
                
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                return JsonSerializer.Deserialize<T>(response, options);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "An error occurred while fetching data from the endpoint {Endpoint}.", endpoint);
                throw new Exception("An error occurred while fetching data from the external API.");
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "An error occurred while deserializing the data from the endpoint {Endpoint}.", endpoint);
                throw new Exception("An error occurred while deserializing the data.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching data from the endpoint {Endpoint}.", endpoint);
                throw;
            }
        }
    }
}