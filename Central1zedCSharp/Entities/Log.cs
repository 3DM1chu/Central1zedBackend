namespace Central1zedCSharp.Entities
{
    public class Log
    {
        public int Id { get; set; }
        public required string Message { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public int EndpointClientId { get; set; }
        public EndpointClient? EndpointClient { get; set; } = null!;
    }
}