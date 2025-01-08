using TransactionsAPI;

namespace TransactionsAPI.Services.Order
{
    public interface IOrderService
    {
        public Task<IEnumerable<Data.Entities.Order>> GetOrders();
        public Task<Data.Entities.Order> GetOrder(int id);
        public Task<Data.Entities.Order> CreateOrder(Data.Entities.Order order);
        public Task<Data.Entities.Order> UpdateOrder(Data.Entities.Order order);
        public Task<bool> DeleteOrder(int OrderId);
    }
}
