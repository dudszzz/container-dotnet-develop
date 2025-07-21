using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using api.src.dataContext;
using api.src.dto.global;
using api.src.dto.login;
using api.src.entities;
using api.src.exceptionError;
using api.src.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.src.controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AuthController : ControllerBase
  {
    private readonly AuthService _authService;
    private readonly DataContext _dataContext;
    private readonly IConfiguration _configuration;
    private readonly PasswordService _passwordService;

    public AuthController(AuthService authService, DataContext dataContext, IConfiguration configuration, PasswordService passwordService)
    {
      _authService = authService;
      _configuration = configuration;
      _dataContext = dataContext;
      _passwordService = passwordService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody] RegisterDTO model)
    {
      var userByEmail = await _authService.GetUserByEmailAsync(_dataContext, model.Email);

      if (userByEmail != null)
        return Conflict(new ResponseDTO { Status = "Error", Message = "User already exist!" });

      try
      {
        var password = _passwordService.CreatePassword(model.Password);

        UserEntity user = new()
        {
          Id = Guid.NewGuid().ToString(),
          Email = model.Email,
          Password = password,
          DateCreate = DateTime.Now,
          DateUpdate = DateTime.Now
        };

        await _dataContext.Users.AddAsync(user);
        await _dataContext.SaveChangesAsync();

        return Created("/register", new ResponseDTO { Status = "Success", Message = "User created!" });
      }
      catch (Exception ex)
      {
        return this.InternalServerError(ex);
      }
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginDTO model)
    {
      var userByEmail = await _authService.GetUserByEmailAsync(_dataContext, model.Email);

      if (userByEmail == null)
        return BadRequest(new ResponseDTO { Status = "Error", Message = "Invalid e-mail or password!" });

      var verifyPassword = _passwordService.verifyPassword(userByEmail.Password!, model.Password);
      if (!verifyPassword)
        return BadRequest(new ResponseDTO { Status = "Error", Message = "Invalid e-mail or password!" });

      if (userByEmail != null && verifyPassword)
      {
        var authClaims = new List<Claim>
        {
          new Claim(ClaimTypes.Email, userByEmail.Email!),
          new Claim(ClaimTypes.NameIdentifier, userByEmail.Id)
        };

        var token = _authService.GenerateAccessToken(authClaims, _configuration);
        
        return Ok(new
        {
          Token = new JwtSecurityTokenHandler().WriteToken(token),
          Expiration = token.ValidTo
        });
      }
      else
      {
        return BadRequest(new ResponseDTO { Status = "Success", Message = "Incorrect email or password" });
      }
    }
  }
}