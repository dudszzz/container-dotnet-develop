using Microsoft.AspNetCore.Identity;

namespace api.src.services
{
  public class PasswordService
  {
    readonly PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

    public string CreatePassword(string password)
    {
      return passwordHasher.HashPassword(null!, password);
    }

    public bool verifyPassword(string passwordSaved, string password)
    {
      var result = passwordHasher.VerifyHashedPassword(null!, passwordSaved, password);

      return result == PasswordVerificationResult.Success;
    }
  }
}