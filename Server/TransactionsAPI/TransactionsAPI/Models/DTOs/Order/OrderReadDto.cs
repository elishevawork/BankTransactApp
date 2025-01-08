using static TransactionsAPI.Consts.Enums.Enums;

namespace TransactionsAPI.Models.DTOs.Order
{
    public class OrderReadDto
    {
        public int OrderId { get; set; }
        public string? CustomerIdNumber { get; set; }
        public string? CustomerFullName { get; set; }    
        public string? CustomerFullNameHebrew { get; set; }
        public DateTime? CustomerDateOfBirth { get; set; }
        public OrderType OrderType { get; set; }
        public Decimal Amount { get; set; }
        public string? AccountNumber { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; }
    }
}
