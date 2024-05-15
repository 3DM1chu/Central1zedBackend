namespace Central1zedCSharp.Entities
{
    public class EndpointClient
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int TokenId { get; set; }
        public required Token Token { get; set; }
        public int ConfirmationId {get;set;}
        public required Confirmation Confirmation {get;set;}
        public List<Log> Logs {get;set;} = [];
    }
}