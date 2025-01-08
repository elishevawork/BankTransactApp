using TransactionsAPI.Models.DTOs.Order;

namespace TransactionsAPI.Handlers
{
    public interface IOrderHandler
    {
        public Task<OrderReadDto> GetOrder(int orderId);
        public Task<IEnumerable<OrderReadDto>> GetOrders();
        public Task<OrderReadDto> CreateOrder(OrderCreateDto orderCreate);
        public Task<OrderReadDto> UpdateOrder(int orderId, OrderUpdateDto orderUpdate );
        public Task<bool> DeleteOrder(int orderId);
    }
}
