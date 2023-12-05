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
                OrderId = request.OrderId,
                RatingId = CreateRandomToken(),
                Comment = request.Comment,
                IsRatingDisplayed = 1,
                RatingPoint = request.RatingPoint,
                UserEmail = request.UserEmail,
                RatingDate = request.RatingDate,

            };

            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
            return Ok("Rating successfully created!");

        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateStore(UpdateRatings request)
        {
            var rate = await _context.Ratings.FirstOrDefaultAsync(u => u.StoreEmail == request.StoreEmail && u.RatingId == request.RatingId );
            if (rate == null)
            {
                return NotFound("Rating not found.");
            }

            rate.IsRatingDisplayed = request.IsRatingDisplayed;
            rate.RatingDisabledComment = request.RatingDisabledComment;

            _context.Ratings.Update(rate);
            await _context.SaveChangesAsync();

            return Ok("Rating updated successfully!");
        }

       

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(4));
        }
    }
}
