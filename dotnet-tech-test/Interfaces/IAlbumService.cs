using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_tech_test.Models;

namespace dotnet_tech_test.Interfaces
{
    public interface IAlbumService
    {
        Task<List<Album>?> GetCombinedDataAsync();
        Task<List<Album>?> GetCombinedDataByUserIdAsync(int userId);
    }
}