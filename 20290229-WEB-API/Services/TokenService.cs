// TokenService.cs
using Azure.Core;
using Models.Token;
using System.Security.Cryptography;

public class TokenService
{
    private readonly DataContext _context;
    private readonly Token _tokenConfiguration;

    public TokenService(DataContext context, Token tokenConfiguration)
    {
        _context = context;
        _tokenConfiguration = tokenConfiguration;
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
            token.AccessToken = CreateRandomToken(20);
            token.AccessTokenExpires = DateTime.Now.AddDays(1);
            _context.SaveChanges();
            return "-3";
        }
        System.Diagnostics.Debug.WriteLine("1");
        // Access token is still valid
        return token.Email;
    }

    public string GenerateAndSaveUserToken(string userEmail)
    {
        var token = _context.Tokens.FirstOrDefault(t => t.Email == userEmail);
        if (token != null)
        {
            _context.Tokens.Remove(token);
        }
        var newAccessToken = CreateRandomToken(20);
        var newRefreshToken = CreateRandomToken(20);

        var newToken = new Token
        {
            Email = userEmail,
            AccessToken = newAccessToken,
            AccessTokenExpires = DateTime.Now.AddDays(1),
            RefreshToken = newRefreshToken,
            RefreshTokenExpires = DateTime.Now.AddDays(5),
        };

        _context.Tokens.Add(newToken);
        _context.SaveChanges();

        return $"{newAccessToken}-{newRefreshToken}";
    }
    private string CreateRandomToken(int lenght)
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(lenght));
    }


}
