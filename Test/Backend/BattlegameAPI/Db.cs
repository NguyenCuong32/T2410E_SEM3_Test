using Microsoft.Extensions.Configuration;

namespace BattlegameAPI;

public class Db
{
    private readonly IConfiguration _config;
    public Db(IConfiguration config) => _config = config;

    public string ConnStr => _config["SqlConnectionString"]!;
}