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
        private readonly TokenService _tokenService;


        public RatingController(DataContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
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
            string UserEmail =  _tokenService.GetUserEmailFromAccessToken(request.AccessToken);
            if (UserEmail == "-2")
            {
                return StatusCode(210, "Refresh Token Expired");
            }
            if (UserEmail == "-3")
            {
                return StatusCode(211, "Refresh Token Expired");
            }
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
                UserEmail = UserEmail,
                RatingDate = request.RatingDate,

            };

            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
            return Ok("Rating successfully created!");

        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateStore(UpdateRatings request)
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
            var rate = await _context.Ratings.FirstOrDefaultAsync(u => u.StoreEmail == StoreEmail && u.RatingId == request.RatingId );
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
