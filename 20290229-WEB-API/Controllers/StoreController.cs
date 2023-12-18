using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
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
            if (_context.Stores.Any(u => u.StoreEmail == request.StoreEmail))
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
            store.StoreCoverImageLink = request.StoreCoverImageLink;
            store.StoreOpeningTime = request.StoreOpeningTime;
            store.StoreClosingTime = request.StoreClosingTime;
            store.Latitude = request.Latitude;
            store.Longitude = request.Longitude;

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

            // Generate a random token
            var resetToken = CreateRandomToken2();

            // Set the password reset token and expiration time in the database
            store.StorePasswordResetToken = resetToken;
            store.StoreResetTokenExpires = DateTime.Now.AddMinutes(5);
            await _context.SaveChangesAsync();

            // Send the password reset email
            await SendPasswordResetEmail(store.StoreEmail, resetToken);

            return Ok("Your reset token sent to your email account. Please check spam folder also.");
        }


        [HttpPost("check-reset-token")]
        public async Task<IActionResult> CheckResetToken(StoreCheckToken request)
        {
            var store = await _context.Stores.FirstOrDefaultAsync(u => u.StorePasswordResetToken == request.StorePasswordResetToken && u.StoreEmail == request.StoreEmail);
            if (store == null || store.StoreResetTokenExpires < DateTime.Now)
            {
                return BadRequest("Invalid Token.");
            }

            return Ok("Token is valid.");
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

            return Ok($"Password successfully reset. {store.StorePasswordResetToken}");
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
                Body = GetPasswordResetEmailBody(resetToken,email),
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
       
        private bool StoreVerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        
        private string CreateRandomToken2()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(3));
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
