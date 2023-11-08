using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Linq;

namespace VerifyEmailForgotPasswordTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly DataContext _context;

        public MenuController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public IActionResult CreateMenuItem([FromBody] CreateMenuItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Create a new menu item from the request
            var newItem = new Menu
            {
                StoreEmail = request.StoreEmail,
                MenuItemName = request.MenuItemName,
                MenuItemId = request.MenuItemId,
                MenuItemCategory = request.MenuItemCategory,
                MenuItemCount = request.MenuItemCount,
                MenuItemPrice = request.MenuItemPrice,
                
            };

            _context.Menus.Add(newItem);
            _context.SaveChanges();

            return Ok("Menu item created successfully.");
        }

        [HttpPost("delete")]
        public IActionResult DeleteMenuItem([FromBody] DeleteMenuItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Find the menu item to delete
            var itemToDelete = _context.Menus.FirstOrDefault(m =>
                m.StoreEmail == request.StoreEmail && m.MenuItemId == request.MenuItemId);

            if (itemToDelete == null)
            {
                return NotFound("Menu item not found.");
            }

            _context.Menus.Remove(itemToDelete);
            _context.SaveChanges();

            return Ok("Menu item deleted successfully.");
        }

        [HttpPost("update")]
        public IActionResult UpdateMenuItem([FromBody] CreateMenuItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Find the menu item to update
            var itemToUpdate = _context.Menus.FirstOrDefault(m =>
                m.StoreEmail == request.StoreEmail && m.MenuItemId == request.MenuItemId);

            if (itemToUpdate == null)
            {
                return NotFound("Menu item not found.");
            }

            // Update the properties as needed
            itemToUpdate.MenuItemName = request.MenuItemName;
            itemToUpdate.MenuItemCategory = request.MenuItemCategory;
            itemToUpdate.MenuItemPrice = request.MenuItemPrice;
            itemToUpdate.MenuItemCount = request.MenuItemCount;

            _context.SaveChanges();

            return Ok("Menu item updated successfully.");
        }
    }
}
