using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using dotnet_tech_test.Interfaces;
using dotnet_tech_test.Models;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace dotnet_tech_test.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IDataCalls _dataCalls;
        private readonly ILogger<AlbumService> _logger;

        public AlbumService(IDataCalls dataCalls, ILogger<AlbumService> logger)
        {
            _dataCalls = dataCalls;
            _logger = logger;
        }

        public async Task<List<Album>?> GetCombinedDataAsync()
        {
            try
            {
                var photos = await _dataCalls.GetData<List<Photo>>("http://jsonplaceholder.typicode.com/photos");
                var albums = await _dataCalls.GetData<List<Album>>("http://jsonplaceholder.typicode.com/albums");

                if (albums == null)
                {
                    _logger.LogError("Failed to fetch albums.");
                    throw new Exception("Failed to fetch albums.");
                }

                if (photos == null)
                {
                    _logger.LogError("Failed to fetch photos.");
                    throw new Exception("Failed to fetch photos.");
                }

                foreach (var album in albums)
                {
                    album.Photos = photos.FindAll(photo => photo.AlbumId == album.Id);
                }

                return albums;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "An error occurred while fetching data from the external API.");
                throw new Exception("An error occurred while fetching data from the external API.");
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "An error occurred while deserializing the data.");
                throw new Exception("An error occurred while deserializing the data.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                throw;
            }
        }

        public async Task<List<Album>?> GetCombinedDataByUserIdAsync(int userId)
        {
            try
            {
                var albums = await GetCombinedDataAsync();
                return albums?.Where(album => album.UserId == userId).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching data for user ID {UserId}.", userId);
                throw;
            }
        }
    }
}