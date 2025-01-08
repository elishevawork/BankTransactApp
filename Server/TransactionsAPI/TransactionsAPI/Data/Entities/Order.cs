using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransactionsAPI.Data.Entities
{
    public class Order
    {        
        public int OrderId { get; set; }
        public string? CustomerIdNumber { get; set; }
        public string? CustomerFullName { get; set; }
        public string? CustomerFullNameHebrew { get; set; }
        public DateTime? CustomerDateOfBirth { get; set; }
        public int OrderType { get; set; }
        public Decimal? Amount { get; set; }
        public string? AccountNumber { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; } = DateTime.Now;
    }
}
