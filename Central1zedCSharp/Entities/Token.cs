namespace Central1zedCSharp.Entities
{
    public enum TokenRole
    {
        Admin = 0,
        Endpoint = 1
    }

    public class Token
    {
        public int Id {get;set;}
        public Guid Guid {get;set;}
        public bool IsActive { get; set; } = false;
        public TokenRole Role { get; set; } = TokenRole.Endpoint;

    }
}