using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Order;
using Models.OrderDetail;
using System.Security.Cryptography;

namespace cemerenbwebapi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly DataContext _context;

        public OrderDetailsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("get-order-details")]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetUserOrders([FromQuery] string OrderId)
        {
            if (string.IsNullOrEmpty(OrderId))
            {
                return BadRequest("Order id parameter is required");
            }

            var orderdetails = await _context.OrderDetails
                .Where(o => o.OrderId == OrderId)
                .ToListAsync();

            if (orderdetails == null || !orderdetails.Any())
            {
                return NotFound("No order details found for the specified " + OrderId);
            }

            return orderdetails;
        }

        [HttpPost("create-order-details")]
        public async Task<IActionResult> CreateOrder(CreateOrderDetail request)
        {

            var orderDetails = new OrderDetail
            {
                StoreEmail = request.StoreEmail,
                OrderId = request.OrderId,
                UserEmail = request.UserEmail,
                MenuItemId = request.MenuItemId,
                ItemCount = request.ItemCount,
                
            };

            _context.OrderDetails.Add(orderDetails);
            await _context.SaveChangesAsync();
            return Ok("Order details added created!");

        }

        



    }
}
