using System.ComponentModel.DataAnnotations;

namespace api.src.dto.login
{
  public class LoginDTO
  {
    [EmailAddress]
    public required string Email { get; set; }
    public required string Password { get; set; }
  }
}