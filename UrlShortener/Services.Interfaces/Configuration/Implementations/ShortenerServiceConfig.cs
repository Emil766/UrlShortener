using Services.Interfaces.Configuration.Interfaces;

namespace Services.Configuration.Implementations
{
    public class ShortenerServiceConfig : IShortenerServiceConfig
    {
        public string SelfBaseUrl { get; set; }
    }
}
