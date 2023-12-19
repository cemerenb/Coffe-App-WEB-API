using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Linq;
using Azure.Core;
using Microsoft.AspNetCore.Cors;
using Models.Cart;
using Models.Order;
using cemerenbwebapi.Migrations;

namespace cemerenbwebapi.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly TokenService _tokenService;
        public CartController(DataContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllCarts([FromQuery] GetCart request)
        {
            try
            {
                string UserEmail =  _tokenService.GetUserEmailFromAccessToken(request.AccessToken);
                if (UserEmail == "-2") {
                    return StatusCode(210,"Refresh Token Expired");
                }
                if (UserEmail == "-3")
                {
                    return StatusCode(211, "Refresh Token Expired");
                }
                var matchedCarts = await _context.Carts.Where(u => u.UserEmail == UserEmail).ToListAsync();

                if (matchedCarts != null && matchedCarts.Any())
                {
                    return Ok(matchedCarts);
                }
                else
                {
                    return NotFound("No carts found for the specified user email.");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("get-item-info")]
        public async Task<IActionResult> GetItemInfo([FromQuery] GetItemInfo request)
        {
            try
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
                var matchedCart = await _context.Carts.Where(u => u.UserEmail == UserEmail && u.MenuItemId == request.MenuItemId).ToListAsync();

                if (matchedCart != null && matchedCart.Any())
                {
                    return Ok(matchedCart);
                }
                else
                {
                    return NotFound("No carts found for the specified user email.");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCart(UpdateCart request)
        {
            try
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
                var matchedCarts = await _context.Carts
                    .Where(u => u.UserEmail == UserEmail && u.MenuItemId == request.MenuItemId)
                    .ToListAsync();

                if (matchedCarts == null || matchedCarts.Count == 0)
                {
                    return NotFound("No carts found for the specified user email and menu item.");
                }

                var matchedCart = matchedCarts.First();

                matchedCart.ItemCount = request.ItemCount;

                await _context.SaveChangesAsync();

                return Ok("Cart item updated successfully");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }




        [HttpPost("add")]
        public IActionResult AddToCart([FromBody] AddToCart request)
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Create a new menu item from the request
            var cart = new Cart
            {
                
                StoreEmail = request.StoreEmail,
                UserEmail = UserEmail,
                MenuItemId = request.MenuItemId,
                ItemCount = 1,

            };

            _context.Carts.Add(cart);
            _context.SaveChanges();

            return Ok("Item added to cart successfully.");
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteMenuItem([FromBody] DeleteFromCart request)
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Find the menu item to delete
            var itemToDelete = _context.Carts.FirstOrDefault(m =>
                m.StoreEmail == request.StoreEmail && m.UserEmail == UserEmail && m.MenuItemId == request.MenuItemId);

            if (itemToDelete == null)
            {
                return NotFound("Menu item not found.");
            }

            _context.Carts.Remove(itemToDelete);
            _context.SaveChanges();

            return Ok("Item deleted from cart successfully.");
        }

        
    }
}