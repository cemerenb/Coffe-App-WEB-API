using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Order;
using System.Security.Cryptography;

namespace cemerenbwebapi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly DataContext _context;

        public OrderController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("get-orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _context.Orders.ToListAsync();
            return Ok(orders);
        }


        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder(CreateOrder request)
        {

            var order = new Order
            {
                StoreEmail = request.StoreEmail,
                OrderId = request.OrderId,
                UserEmail = request.UserEmail,
                OrderStatus = request.OrderStatus,
                OrderNote = request.OrderNote,
                OrderCreatingTime = request.OrderCreatingTime,
                OrderTotalPrice = request.OrderTotalPrice,
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return Ok("Order successfully created!");

        }

        [HttpPut("update-status")]
        public async Task<IActionResult> ToggleIsActive(UpdateOrderStatus request)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(u => u.OrderId == request.OrderId);
            if (order == null)
            {
                return NotFound("Order not found.");
            }

            order.OrderStatus = request.OrderStatus;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return Ok("Order status updated successfully!");
        }


        
    }
}
