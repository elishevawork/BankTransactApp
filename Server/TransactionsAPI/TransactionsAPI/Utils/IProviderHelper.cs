using TransactionsAPI.Models.DTOs.Order;
using TransactionsAPI.Models.DTOs.Provider;

namespace TransactionsAPI.Utils
{
    public interface IProviderHelper
    {
        Task<ProviderDataReadDto> PostAsync<T_Content>(string url, T_Content content, string token = "");
        Task<bool> CreateTransactAsync(string customerId, string endpoint, ProviderTransactCreateDto createDto);
        Task<string> GetTokenAsync(string customerId);
    }
}
