using Microsoft.Extensions.Configuration;

namespace SpyStore.MVC.DataAccess
{
    public static partial class WebAPICalls
    {
        public static string ServiceAddress { get; set; }
        public static string ImageLocation { get; set; }
        static WebAPICalls()
        {
            //var builder = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json");
            //var configuration = builder.Build();
            //var customSection = configuration?.GetSection("CustomSettings");
            //ServiceAddress = customSection?.GetSection("ServiceAddress")?.Value;
            //ImageLocation = customSection?.GetSection("ImageLocation")?.Value;
        }
    }
}
