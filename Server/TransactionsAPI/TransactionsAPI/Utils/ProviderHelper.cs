
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using TransactionsAPI.Models.DTOs.Order;
using TransactionsAPI.Models.DTOs.Provider;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TransactionsAPI.Consts;

namespace TransactionsAPI.Utils
{
    public class ProviderHelper : IProviderHelper
    {
        private readonly ILogger _logger;

        public ProviderHelper(ILogger<ProviderHelper> logger)
        {
            _logger = logger;
        }

        public async Task<ProviderDataReadDto> PostAsync<T_Content>(string url, T_Content content, string token = "")
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    if (!string.IsNullOrEmpty(token))
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    httpClient.Timeout = TimeSpan.FromMilliseconds(30); // assume url is demo

                    var json = JsonConvert.SerializeObject(content);
                    var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(url, jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();
                        var responseData = JsonConvert.DeserializeObject<ProviderDataReadDto>(responseBody);
                        return responseData ?? null;
                    }
                    else
                    {
                        return SimulateProviderResponse();
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message, ex);
                return SimulateProviderResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return  SimulateProviderResponse();
            }
        }

        public async Task<bool> CreateTransactAsync(string customerId, string endpoint, ProviderTransactCreateDto createDto)
        {
            var token = await GetTokenAsync(customerId);
            var url = $"{ConfigurationKeys.PROVIDER_BASE_URL}{endpoint}";

            var res = await PostAsync(url, createDto, token);

            if (res != null && res.Code == 200)
                return true;
            return false;
        }

        public async Task<string> GetTokenAsync(string customerId)
        {
            var createDto = new ProviderTokenCreateDto() { UserId = customerId, SecretId = ConfigurationKeys.SECRET_ID };
            string token = string.Empty;
            string url = $"{ConfigurationKeys.PROVIDER_BASE_URL}token";

            var res = await PostAsync(url, createDto);
            if (res != null && res.Code == 200)
                token = res.Data;

            return token;
        }

        private ProviderDataReadDto SimulateProviderResponse()
        {
            return new ProviderDataReadDto { Code = 200, Data = "mock-token123456789 OR Transact Executed" };
        }


    }
}
