using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Cart;
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
        private readonly TokenService _tokenService;


        public OrderDetailsController(DataContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpGet("get-order-details")]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetUserOrders()
        {
            var orderdetails = await _context.OrderDetails.ToListAsync();
            return orderdetails;
        }

        [HttpPut("cancel-order-item")]
        public async Task<IActionResult> CancelOrder(CancelOrderDetails request)
        {
            try
            {
                string StoreEmail =  _tokenService.GetUserEmailFromAccessToken(request.AccessToken);
                if (StoreEmail == "-2")
                {
                    return StatusCode(210, "Refresh Token Expired");
                }
                if (StoreEmail == "-3")
                {
                    return StatusCode(211, "Refresh Token Expired");
                }
                var matchedOrderItem = await _context.OrderDetails
                    .Where(u => u.StoreEmail == StoreEmail && u.MenuItemId == request.MenuItemId && u.OrderId == request.OrderId)
                    .ToListAsync();

                if (matchedOrderItem == null || matchedOrderItem.Count == 0)
                {
                    return NotFound("No order found");
                }

                var item = matchedOrderItem.First();

                item.ItemCanceled = request.ItemCanceled;
                item.CancelNote = request.CancelNote;

                await _context.SaveChangesAsync();

                return Ok("Order item updated successfully");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("create-order-details")]
        public async Task<IActionResult> CreateOrder(CreateOrderDetail request)
        {
            string UserEmail =  _tokenService.GetUserEmailFromAccessToken(request.AccessToken);
            if (UserEmail == "-2")
            {
                return StatusCode(210, "Refresh Token Expired");
            }
            if (UserEmail == "-3")
            {
                return StatusCode(211, "Refresh Token Expired");
            }
            var orderDetails = new OrderDetail
            {
                StoreEmail = request.StoreEmail,
                OrderId = request.OrderId,
                UserEmail = UserEmail,
                MenuItemId = request.MenuItemId,
                ItemCount = request.ItemCount,
                
                
            };

            _context.OrderDetails.Add(orderDetails);
            await _context.SaveChangesAsync();
            return Ok("Order details added created!");

        }

        



    }
}
