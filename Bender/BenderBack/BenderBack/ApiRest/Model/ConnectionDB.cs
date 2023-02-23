using Microsoft.Extensions.Configuration;

namespace ApiRest.Model
{
    public class ConnectionDB
    {
        public static string BenderConnectionString = "";
        public static string SetBenderConnectionString(IConfiguration config)
        {
            BenderConnectionString = config.GetConnectionString("BenderConnectionString");
            return BenderConnectionString;
        }
    }
}
