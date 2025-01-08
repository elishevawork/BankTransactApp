using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using TransactionsAPI.Data.Entities;
using TransactionsAPI.Models.DTOs.Order;
using TransactionsAPI.Models.DTOs.Provider;
using TransactionsAPI.Services.Order;
using TransactionsAPI.Utils;
using static TransactionsAPI.Consts.Enums.Enums;

namespace TransactionsAPI.Handlers
{
    public class OrderHandler : IOrderHandler
    {
        private readonly IOrderService _orderService;
        public readonly IProviderHelper _providerHelper;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderHandler> _logger;
        public OrderHandler(IOrderService orderService, IProviderHelper providerHelper, IMapper mapper, ILogger<OrderHandler> logger)
        {
            _orderService = orderService;
            _providerHelper = providerHelper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OrderReadDto> GetOrder(int orderId)
        {
            var order = await _orderService.GetOrder(orderId);
            return _mapper.Map<OrderReadDto>(order);
        }
        public async Task<IEnumerable<OrderReadDto>> GetOrders()
        {
            var orders = await _orderService.GetOrders();
            var ordersReadDto = orders.Select(o => _mapper.Map<OrderReadDto>(o));
            return ordersReadDto;
        }
        public async Task<OrderReadDto> CreateOrder(OrderCreateDto orderCreateDto)
        {
            var order = _mapper.Map<Order>(orderCreateDto);
            order.Status = (int)OrderStatus.Pending;
            order = await _orderService.CreateOrder(order);
            if (order == null) return null;

            if (await CreateTransactOpenBankProviderAsync(order))
            {
                order.Status = (int)OrderStatus.Executed;
                order = await _orderService.UpdateOrder(order);
            }

            var orderReadDto = _mapper.Map<OrderReadDto>(order);
            return orderReadDto;
        }

        public async Task<OrderReadDto> UpdateOrder(int orderId, OrderUpdateDto orderUpdate)
        {
            var order = await _orderService.GetOrder(orderId);
            if (order == null) { return null; }

            order = await _orderService.UpdateOrder(order);
            var orderReadDto = _mapper.Map<OrderReadDto>(order);
            return orderReadDto;
        }

        public async Task<bool> DeleteOrder(int orderId)
        {
            return await _orderService.DeleteOrder(orderId);
        }

        public async Task<bool> CreateTransactOpenBankProviderAsync(Order order)
        {
            if (order == null || string.IsNullOrEmpty(order.CustomerIdNumber))
                return false;

            var createDto = new ProviderTransactCreateDto() { Amount = order.Amount, AccountNumber = order.AccountNumber };
            return await _providerHelper.CreateTransactAsync(order.CustomerIdNumber, order.OrderType.ToString(), createDto);
        }
    }
}
