using UrlShortener.Configuration.Interafces;

namespace UrlShortener.Configuration.Implementations
{
    public class AppConfig : IAppConfig
    {
        public string SelfBaseUrl { get; set; }
    }
}
