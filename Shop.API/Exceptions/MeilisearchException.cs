namespace Shop.API.Exceptions
{
    public class MeilisearchException : Exception
    {
        public MeilisearchException(string message) : base(message) { }
    }
}
