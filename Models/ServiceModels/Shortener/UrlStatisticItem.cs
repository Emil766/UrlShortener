namespace Models.ServiceModels.Shortener
{
    public class UrlStatisticItem
    {
        public string Id { get; set; }
        public string LongUrl { get; set; }
        public string ShortUrl { get; set; }
        public int RedirectionsCount { get; set; }
    }
}
