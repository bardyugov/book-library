using BookLibrary.Application.Services;
using Microsoft.Extensions.Configuration;

namespace BookLibrary.Infrastructure.Services.ConfigService;

public class ConfigService : IConfigService
{
    private readonly IConfigurationRoot _config = 
        new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();
    
    public string GetValue(string key)
    {
        return _config.GetSection(key).Value;
    }
}