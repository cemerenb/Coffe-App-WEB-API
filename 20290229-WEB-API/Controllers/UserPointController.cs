using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Point;
using System.Security.Cryptography;

namespace cemerenbwebapi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserPointsController : ControllerBase
    {
        private readonly DataContext _context;

        public UserPointsController(DataContext context)
        {
            _context = context;
        }





        [HttpPost("create-user-point")]
        public async Task<IActionResult> CreateUserPoint(CreateUserPointData request)
        {
            if (_context.Points.Any(u => u.StoreEmail == request.StoreEmail && u.UserEmail == request.UserEmail))
            {
                return BadRequest("This user already have points for this store");
            }



            var point = new Point
            {
                StoreEmail = request.StoreEmail,
                UserEmail = request.UserEmail,
                TotalPoint = request.TotalPoint,
                TotalGained = request.TotalGained,

            };

            _context.Points.Add(point);
            await _context.SaveChangesAsync();
            return Ok("User point created successfully");

        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUserPoints(UpdateUserPoints request)
        {
            var point = await _context.Points.FirstOrDefaultAsync(u => u.StoreEmail == request.StoreEmail && u.UserEmail == request.UserEmail);
            if (point == null)
            {
                return NotFound("User don't have point for this store");
            }

            point.TotalGained = request.TotalGained;
            point.TotalPoint = request.TotalPoint;

            _context.Points.Update(point);
            await _context.SaveChangesAsync();

            return Ok("Points gained successfully!");
        }




    }
}
