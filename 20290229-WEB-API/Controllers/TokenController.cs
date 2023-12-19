using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Linq;
using Azure.Core;
using Microsoft.AspNetCore.Cors;
using Models.Cart;
using Models.Order;
using cemerenbwebapi.Migrations;
using Models.Token;

namespace cemerenbwebapi.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly TokenService _tokenService;
        public TokenController(DataContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        

        
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCart(UpdateAccessToken request)
        {
            try
            {

                var accessToken = await _context.Tokens
                    .FirstOrDefaultAsync(u => u.RefreshToken == request.RefreshToken);
                    

                if (accessToken == null)
                {
                    return NotFound("No token found");
                    
                }

                if (accessToken.RefreshTokenExpires < DateTime.Now)
                {
                    return StatusCode(210);
                }
                string token = CreateRandomToken(20);
                accessToken.AccessToken = token;
                accessToken.AccessTokenExpires = DateTime.Now.AddDays(1);
                await _context.SaveChangesAsync();

                return Ok(token);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        private string CreateRandomToken(int lenght)
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(lenght));
        }




    }
}