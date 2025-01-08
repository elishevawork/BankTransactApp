
using Microsoft.EntityFrameworkCore;
using TransactionsAPI.Data;
using static TransactionsAPI.Consts.Enums.Enums;

namespace TransactionsAPI.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly TransactionsDbContext _context;
        private readonly ILogger<OrderService> _logger;
        public OrderService(TransactionsDbContext context, ILogger<OrderService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Data.Entities.Order> GetOrder(int id)
        {
            return await _context.Orders.FirstOrDefaultAsync(O => O.OrderId == id);
        }

        public async Task<IEnumerable<Data.Entities.Order>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }
        public async Task<Data.Entities.Order> CreateOrder(Data.Entities.Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync(); //Assume result > 0
            return order;
        }
        public async Task<Data.Entities.Order> UpdateOrder(Data.Entities.Order order)
        {
            _context.Orders.Update(order);
            var res = await _context.SaveChangesAsync();
            return res > 0 ? order : null;
        }
        public async Task<bool> DeleteOrder(int OrderId)
        {
            var order = await _context.Orders.Where(o => o.OrderId == OrderId).FirstOrDefaultAsync();
            if (order == null) return false;

            _context.Orders.Remove(order);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
