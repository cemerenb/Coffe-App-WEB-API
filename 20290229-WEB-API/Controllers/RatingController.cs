using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Rating;
using System.Security.Cryptography;

namespace cemerenbwebapi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly DataContext _context;

        public RatingController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllStores()
        {
            var ratings = await _context.Ratings.ToListAsync();
            return Ok(ratings);
        }




        [HttpPost("add-rating")]
        public async Task<IActionResult> AddRating(AddRatings request)
        {
            if (_context.Ratings.Any(u => u.OrderId == request.OrderId))
            {
                return BadRequest("This order already commented");
            }



            var rating = new Rating
            {
                StoreEmail = request.StoreEmail,
                StoreName = request.StoreName,
                OrderId = request.OrderId,
                RatingId = CreateRandomToken(),
                Comment = request.Comment,
                IsRatingDisplayed = 1,
                RatingPoint = request.RatingPoint,
                UserEmail = request.UserEmail,
                UserFullName = request.UserFullName,
                RatingDate = request.RatingDate,

            };

            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
            return Ok("Rating successfully created!");

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
            store.StoreOpeningTime = request.StoreOpeningTime;
            store.StoreClosingTime = request.StoreClosingTime;

            _context.Stores.Update(store);
            await _context.SaveChangesAsync();

            return Ok("Store updated successfully!");
        }

        [HttpPut("toggle-store")]
        public async Task<IActionResult> ToggleIsActive(StoreToggleIsOn request)
        {
            var store = await _context.Stores.FirstOrDefaultAsync(u => u.StoreEmail == request.StoreEmail);
            if (store == null)
            {
                return NotFound("Store not found.");
            }


            store.StoreIsOn = request.StoreIsOn;

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
