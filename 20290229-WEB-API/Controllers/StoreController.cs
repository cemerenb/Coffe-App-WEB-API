using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace cemerenbwebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly DataContext _context;

        public StoreController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllStores()
        {
            var stores = await _context.Stores.ToListAsync();
            return Ok(stores);
        }


        [HttpPost("register")]
        public async Task<IActionResult> StoreRegister(StoreRegisterRequest request)
        {
            if (_context.Stores.Any(u => u.StoreEmail == request.StoreEmail) || _context.Stores.Any(u => u.StoreName == request.StoreName))
            {
                return BadRequest("User already exists.");
            }

            CreatePasswordHash(request.StorePassword,
                 out byte[] passwordHash,
                 out byte[] passwordSalt);

            var store = new Store
            {
                StoreName = request.StoreName,
                StoreTaxId = request.StoreTaxId,
                StoreEmail = request.StoreEmail,
                StorePasswordHash = passwordHash,
                StorePasswordSalt = passwordSalt,
            };

            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
            return Ok("Store successfully created!");

        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateStore(StoreUpdateRequest request)
        {
            var store = await _context.Stores.FirstOrDefaultAsync(u => u.StoreEmail == request.StoreEmail);
            if (store == null)
            {
                return NotFound("Store not found.");
            }

            
            store.StoreLogoLink = request.StoreLogoLink;
            store.StoreIsOn = request.StoreIsOn;
            store.StoreOpeningTime = request.StoreOpeningTime;
            store.StoreClosingTime = request.StoreClosingTime;

            _context.Stores.Update(store);
            await _context.SaveChangesAsync();

            return Ok("Store updated successfully!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> StoreLogin(StoreLoginRequest request)
        {
            var store = await _context.Stores.FirstOrDefaultAsync(u => u.StoreEmail == request.StoreEmail);
            if (store == null)
            {
                return BadRequest("Store not found.");
            }

            if (!StoreVerifyPasswordHash(request.StorePassword, store.StorePasswordHash, store.StorePasswordSalt))
            {
                return BadRequest("Password is incorrect.");
            }

            return Ok($"Welcome back, {store.StoreEmail}!");
        }

     

        [HttpPost("forgot-password")]
        public async Task<IActionResult> StoreForgotPassword(string email)
        {
            var store = await _context.Stores.FirstOrDefaultAsync(u => u.StoreEmail == email);
            if (store == null)
            {
                return BadRequest("Store not found.");
            }

            store.StorePasswordResetToken = CreateRandomToken();
            store.StoreResetTokenExpires = DateTime.Now.AddDays(1);
            await _context.SaveChangesAsync();

            return Ok("You may now reset your password.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResettPassword(StoreResetPasswordRequest request)
        {
            var store = await _context.Stores.FirstOrDefaultAsync(u => u.StorePasswordResetToken == request.StoreToken);
            if (store == null || store.StoreResetTokenExpires < DateTime.Now)
            {
                return BadRequest("Invalid Token.");
            }

            CreatePasswordHash(request.StorePassword, out byte[] passwordHash, out byte[] passwordSalt);

            store.StorePasswordHash = passwordHash;
            store.StorePasswordSalt = passwordSalt;
            store.StorePasswordResetToken = null;
            store.StoreResetTokenExpires = null;

            await _context.SaveChangesAsync();

            return Ok("Password successfully reset.");
        }
        [HttpGet("{storeEmail}/orders")]
        public async Task<IActionResult> GetAllOrdersForStore(string storeEmail)
        {
            var orders = await _context.Orders.Where(o => o.Store == storeEmail).ToListAsync();
            return Ok(orders);
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
       
        private bool StoreVerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(4));
        }
    }
}
