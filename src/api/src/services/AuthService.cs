using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.src.dataContext;
using api.src.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace api.src.services
{
  public class AuthService
  {
    public JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration _configuration)
    {
      var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]!));

      JwtSecurityToken token = new JwtSecurityToken(
        issuer: _configuration["JWT:ValidIssuer"],
        audience: _configuration["JWT:ValidAudience"],
        claims: claims,
        notBefore: DateTime.Now,
        expires: DateTime.Now.AddHours(double.Parse(_configuration["JWT:ExpireHours"]!)),
        signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
      );

      return token;
    }

    public async Task<UserEntity?> GetUserByEmailAsync(DataContext _context, string Email)
    {
      return await _context.Users.FirstOrDefaultAsync(u => u.Email == Email);
    }
  }

}
