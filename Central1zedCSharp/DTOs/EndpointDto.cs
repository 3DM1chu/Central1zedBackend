using Central1zedCSharp.Entities;

namespace Central1zedCSharp.DTOs
{
    public record class EndpointDto
    (
        string Name,
        ICollection<Log>? Logs
    );
}