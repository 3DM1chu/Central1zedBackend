namespace Central1zedCSharp.DTOs;

public record class LogToAddDto(
    string Message,
    DateTime? Time,
    string EndpointName
);