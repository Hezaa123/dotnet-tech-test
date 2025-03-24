using System.Threading.Tasks;

namespace dotnet_tech_test.Interfaces;

public interface IDataCalls
{
    Task<T?> GetData<T>(string endpoint);
}