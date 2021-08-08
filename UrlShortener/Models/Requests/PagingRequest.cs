namespace UrlShortener.Requests
{
    public abstract class PagingRequest
    {
        public int Take { get; set; }
        public string LastId { get; set; }
    }
}
