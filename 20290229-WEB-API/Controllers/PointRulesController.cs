using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.PointRule;
using System.Security.Cryptography;

namespace cemerenbwebapi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PointRulesController : ControllerBase
    {
        private readonly DataContext _context;

        public PointRulesController(DataContext context)
        {
            _context = context;
        }


        [HttpGet("get-all")]
        public async Task<IActionResult> GetRules()
        {

            var rule = await _context.PointRules.ToListAsync();
            return Ok(rule);
        }


        [HttpPost("add-point-rule")]
        public async Task<IActionResult> AddPointRule(AddPointRule request)
        {
            if (_context.PointRules.Any(u => u.StoreEmail == request.StoreEmail))
            {
                return BadRequest("This store already defined rules");
            }



            var pointRules = new PointRule
            {
                StoreEmail = request.StoreEmail,
                IsPointsEnabled = request.IsPointsEnabled,
                PointsToGain = request.PointsToGain,
                Category1Gain = request.Category1Gain,
                Category2Gain = request.Category2Gain,  
                Category3Gain = request.Category3Gain,  
                Category4Gain = request.Category4Gain,

            };

            _context.PointRules.Add(pointRules);
            await _context.SaveChangesAsync();
            return Ok("Rules successfully saved");

        }
        [HttpPut("toggle-loyalty-status")]
        public async Task<IActionResult> ToggleIsActive(ToggleLoyaltyStatus request)
        {
            var rule = await _context.PointRules.FirstOrDefaultAsync(u => u.StoreEmail == request.StoreEmail);
            if (rule == null)
            {
                return NotFound("Store don't have rule.");
            }


            rule.IsPointsEnabled = request.IsPointsEnabled;

            _context.PointRules.Update(rule);
            await _context.SaveChangesAsync();

            return Ok("Loyalty status changed successfully!");
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateRules(UpdatePointRules request)
        {
            var rule = await _context.PointRules.FirstOrDefaultAsync(u => u.StoreEmail == request.StoreEmail);
            if (rule == null)
            {
                return NotFound("Store don't have point rules");
            }

            rule.IsPointsEnabled = request.IsPointsEnabled;
            rule.PointsToGain = request.PointsToGain;
            rule.Category1Gain = request.Category1Gain;
            rule.Category2Gain = request.Category2Gain;
            rule.Category3Gain = request.Category3Gain;
            rule.Category4Gain = request.Category4Gain;

            _context.PointRules.Update(rule);
            await _context.SaveChangesAsync();

            return Ok("Store point rules updated successfully!");
        }



        
    }
}
