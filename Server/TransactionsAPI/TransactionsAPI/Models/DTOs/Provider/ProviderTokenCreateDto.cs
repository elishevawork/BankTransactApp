using TransactionsAPI.Consts;

namespace TransactionsAPI.Models.DTOs.Provider
{
    public class ProviderTokenCreateDto
    {
        public string UserId { get; set; }
        public string? SecretId { get; set; }
    }
}
