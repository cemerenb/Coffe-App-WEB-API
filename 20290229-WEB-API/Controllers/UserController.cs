using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Menu;
using Models.OrderDetail;
using Models.User;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using Models.Token;

namespace cemerenbwebapi.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("get-users-name")]
        public IActionResult GetUserName([FromQuery] string email)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == email);
          
            if (user == null)
            {
                return NotFound("User not found.");
            }
            
            return Ok(user.FullName);
        }

        

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            if (_context.Users.Any(u => u.Email == request.Email))
            {
                return BadRequest("User already exists.");
            }

            CreatePasswordHash(request.Password,
                 out byte[] passwordHash,
                 out byte[] passwordSalt);

            var user = new User
            {   
                FullName = request.Fullname,
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User successfully created!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Password is incorrect.");
            }

            

            return Ok($"Welcome back, {user.Email}! :)");
        }




        [HttpPost("forgot-password")]
        public async Task<IActionResult> StoreForgotPassword(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return BadRequest("Store not found.");
            }

            // Generate a random token
            var resetToken = CreateRandomToken2();

            // Set the password reset token and expiration time in the database
            user.PasswordResetToken = resetToken;
            user.ResetTokenExpires = DateTime.Now.AddMinutes(5);
            await _context.SaveChangesAsync();

            // Send the password reset email
            await SendPasswordResetEmail(user.Email, resetToken);

            return Ok("Your reset token sent to your email account. Please check spam folder also.");
        }

        [HttpPost("check-reset-token")]
        public async Task<IActionResult> CheckResetToken(UserCheckToken request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == request.PasswordResetToken && u.Email == request.Email);
            if (user == null || user.ResetTokenExpires < DateTime.Now)
            {
                return BadRequest("Invalid Token.");
            }

            return Ok("Token is valid.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResettPassword(ResetPasswordRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);
            if (user == null || user.ResetTokenExpires < DateTime.Now)
            {
                return BadRequest("Invalid Token.");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.PasswordResetToken = null;
            user.ResetTokenExpires = null;

            await _context.SaveChangesAsync();

            return Ok("Password successfully reset.");
        }
        private async Task SendPasswordResetEmail(string email, string resetToken)
        {
            // Configure your SMTP server settings
            var smtpClient = new SmtpClient("smtp-relay.brevo.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("200704020@st.maltepe.edu.tr", "jEy2JnhRzLDxcX5V"),
                EnableSsl = true,
            };

            // Create the email message
            var mailMessage = new MailMessage
            {
                From = new MailAddress("200704020@st.maltepe.edu.tr"),
                Subject = "Password Reset",
                Body = GetPasswordResetEmailBody(resetToken, email),
                IsBodyHtml = false,
            };

            // Add the recipient's email address
            mailMessage.To.Add(email);

            try
            {
                // Send the email
                await smtpClient.SendMailAsync(mailMessage);
                Console.WriteLine("Password reset email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send password reset email. Error: {ex.Message}");
            }
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
        
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public string GetUserEmailFromAccessToken(string accessToken)
        {
            var token = _context.Tokens.FirstOrDefault(t => t.AccessToken == accessToken);

            if (token == null)
            {
                
                return "-1";
            }

            if (token.AccessTokenExpires < DateTime.Now)
            {
                if (token.RefreshTokenExpires < DateTime.Now)
                {
                    // Both access and refresh tokens are expired; delete the token data
                    _context.Tokens.Remove(token);
                    _context.SaveChanges();
                    return "-2";
                }

                // Access token expired, but refresh token is valid
                token.AccessToken = CreateUserToken();
                token.AccessTokenExpires = DateTime.Now.AddDays(1);
                _context.SaveChanges();
                return token.Email;
            }

            // Access token is still valid
            return token.Email;
        }

        public string GenerateAndSaveUserToken(string userEmail)
        {
            var newAccessToken = CreateUserToken();

            var token = new Token
            {
                Email = userEmail,
                AccessToken = newAccessToken,
                AccessTokenExpires = DateTime.Now.AddDays(1),
                RefreshToken = CreateUserToken(),
                RefreshTokenExpires = DateTime.Now.AddDays(5),
            };

            _context.Tokens.Add(token);
            _context.SaveChanges();

            return newAccessToken;
        }



        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(4));
        }
        private string CreateRandomToken2()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(3));
        }
        private string CreateUserToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(8));
        }
        private string GetPasswordResetEmailBody(string resetToken, string email)
        {
            // Your HTML template goes here
            var htmlBody = $@"
        <!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Password Reset</title>
</head>
<body>
    <div class=""container"" style=""max-width: 600px; margin: 0 auto; padding: 20px; background-color: #ffffff; border-radius: 8px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); margin-top: 20px; text-align: center; font-family: 'Arial', sans-serif; background-color: #f8f8f8; margin: 0; padding: 0;"">
        <img src=""https://i.ibb.co/KzM2Qs4/coffee-logo-wide.png"" alt=""Coffeee"" class=""logo"" style=""max-width: 100%; height: auto; margin-bottom: 20px;"">
        <h1 style=""color: #4e4e4e;"">Password Reset</h1>
        <p style=""color: #666666; margin-bottom: 20px;"">Hello {email},</p>
        <p style=""color: #666666; margin-bottom: 20px;"">We received a request to reset your password. To proceed, please click the button below:</p>

        <p style=""margin-bottom: 70px; margin-top: 70px;"">
            <a style=""display: inline-block; padding: 10px 20px; background-color: #2c3e50; color: #ffffff; text-decoration: none; border-radius: 5px;"">{resetToken}</a>
        </p>

        <p style=""color: #666666;"">If you didn't request a password reset, you can ignore this email. The link is valid for a limited time for security reasons.</p>

        <p>Thank you for choosing Coffeee!</p>
    </div>

    <div class=""footer"" style=""margin-top: 20px; text-align: center; color: #888888;"">
        <p>This email was sent from Coffeee. &copy; 2023 Coffeee. All rights reserved.</p>
    </div>
</body>
</html>


";

            return htmlBody;
        }
    }
}
