using System.ComponentModel.DataAnnotations;

namespace api.src.dto.login
{
  public class RegisterDTO
  {
    [EmailAddress]
    public required string Email { get; set; }
    public required string Password { get; set; }
  }
}