using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_tech_test.Interfaces;
using dotnet_tech_test.Models;
using dotnet_tech_test.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace dotnet_tech_test.Tests.Services
{
    public class AlbumServiceTests
    {
        private readonly Mock<IDataCalls> _mockDataCalls;
        private readonly AlbumService _albumService;

        public AlbumServiceTests()
        {
            _mockDataCalls = new Mock<IDataCalls>();
            Mock<ILogger<AlbumService>> mockLogger = new();
            _albumService = new AlbumService(_mockDataCalls.Object, mockLogger.Object);
        }

        [Fact]
        public async Task GetCombinedDataAsync_ReturnsCombinedData()
        {
            // Arrange
            var photos = new List<Photo>
            {
                new() { Id = 1, AlbumId = 1, Title = "Photo 1", Url = "http://example.com/photo1", ThumbnailUrl = "http://example.com/photo1_thumb" },
                new() { Id = 2, AlbumId = 1, Title = "Photo 2", Url = "http://example.com/photo2", ThumbnailUrl = "http://example.com/photo2_thumb" }
            };

            var albums = new List<Album>
            {
                new() { Id = 1, UserId = 1, Title = "Album 1" }
            };

            _mockDataCalls.Setup(x => x.GetData<List<Photo>>("http://jsonplaceholder.typicode.com/photos")).ReturnsAsync(photos);
            _mockDataCalls.Setup(x => x.GetData<List<Album>>("http://jsonplaceholder.typicode.com/albums")).ReturnsAsync(albums);

            // Act
            var result = await _albumService.GetCombinedDataAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(2, result[0].Photos.Count);
        }

        [Fact]
        public async Task GetCombinedDataByUserIdAsync_ReturnsFilteredData()
        {
            // Arrange
            var photos = new List<Photo>
            {
                new() { Id = 1, AlbumId = 1, Title = "Photo 1", Url = "http://example.com/photo1", ThumbnailUrl = "http://example.com/photo1_thumb" },
                new() { Id = 2, AlbumId = 1, Title = "Photo 2", Url = "http://example.com/photo2", ThumbnailUrl = "http://example.com/photo2_thumb" }
            };

            var albums = new List<Album>
            {
                new() { Id = 1, UserId = 1, Title = "Album 1" },
                new() { Id = 2, UserId = 2, Title = "Album 2" }
            };

            _mockDataCalls.Setup(x => x.GetData<List<Photo>>("http://jsonplaceholder.typicode.com/photos")).ReturnsAsync(photos);
            _mockDataCalls.Setup(x => x.GetData<List<Album>>("http://jsonplaceholder.typicode.com/albums")).ReturnsAsync(albums);

            // Act
            var result = await _albumService.GetCombinedDataByUserIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(1, result[0].UserId);
            Assert.Equal(2, result[0].Photos.Count);
        }
    }
}