using Microsoft.Extensions.Configuration;

namespace Actions.Core.Domain.Shared
{
    public class AppSettings
    {
        public static string SecretKey;
        public static int TokenExpirationInHours;
        public static bool HideActionButtons { get; set; }

        public static void Build(IConfiguration config)
        {
            // Authorization
            SecretKey = config["Authorization:SecretKey"];
            TokenExpirationInHours = string.IsNullOrWhiteSpace(config["Authorization:ExpirationInHours"]) ? 1 : int.Parse(config["Authorization:ExpirationInHours"]);

            // Swagger
            HideActionButtons = string.IsNullOrWhiteSpace(config["Swagger:hideActionButtons"]) ? false : bool.Parse(config["Swagger:hideActionButtons"]);

        }
    }
}
