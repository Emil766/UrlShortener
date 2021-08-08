namespace Services.Interfaces.Configuration.Interfaces
{
    public interface IShortenerServiceConfig
    {
        /// <summary>
        /// URL приложения для сборки короткой ссылки
        /// </summary>
        string SelfBaseUrl { get; set; }
    }
}
