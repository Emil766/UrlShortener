using System;

namespace Models.RepositoryModels.ShortUrlRepository
{
    public class ShortUrl
    {
        public string Id { get; set; }
        public string LongUrl { get; set; }
        public int RedirectionsCount { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastRedirectDate { get; set; }
    }
}
