using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using TransactionsAPI.Data;
using TransactionsAPI.Data.Entities;
using TransactionsAPI.Handlers;
using TransactionsAPI.Models.DTOs.Order;
using TransactionsAPI.Consts;

namespace TransactionsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly TransactionsDbContext _context;
        private readonly IOrderHandler _orderHandler;
        private readonly ILogger<OrderController> _logger;

        public OrderController(TransactionsDbContext transactionsDbContext, IOrderHandler orderHandler, ILogger<OrderController> logger)
        {
            _context = transactionsDbContext;
            _orderHandler = orderHandler;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> GetOrders()
        {
            var orders = await _orderHandler.GetOrders();
            if (orders == null || !orders.Any()) { return NoContent(); }
            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult> GetOrder(int orderId)
        {
            var order = await _orderHandler.GetOrder(orderId);
            if (order == null) { return NotFound(); }
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderReadDto>> CreateOrder([FromBody] OrderCreateDto order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = Constants.ERROR_MESSAGE_404_INVALID_REQUEST });
            }
            //validation
            var orderReadDto = await _orderHandler.CreateOrder(order);
            return Ok(orderReadDto);
        }

        [HttpPut("{orderId}")]
        public async Task<ActionResult> UpdateOrder(int orderId, OrderUpdateDto order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = Constants.ERROR_MESSAGE_404_INVALID_REQUEST });
            }
            var orderReadDto = await _orderHandler.UpdateOrder(orderId, order);
            if (orderReadDto == null) { return NotFound(); }
            return Ok(orderReadDto);
        }

        [HttpDelete("{orderId}")]
        public async Task<ActionResult> DeleteOrder(int orderId)
        {
            if (await _orderHandler.DeleteOrder(orderId))
                return Ok();
            return BadRequest();
        }
    }
}
