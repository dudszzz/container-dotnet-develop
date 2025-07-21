using api.src.services;

namespace api.src.configs
{
  public static class ServicesConfigs
  {
    public static void AddServicesConfigs(this WebApplicationBuilder builder)
    {
      builder.Services.AddScoped<AuthService>()
                      .AddScoped<PasswordService>();
    }
  }
}