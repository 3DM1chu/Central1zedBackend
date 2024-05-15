using Central1zedCSharp.Data;
using Central1zedCSharp.DTOs;
using Central1zedCSharp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Central1zedCSharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EndpointController: ControllerBase
{
    public Token? GetTokenFromHeaders(IHeaderDictionary headers, EndpointContext dbContext){
        bool tokenInHeaders = headers.TryGetValue("apikey", out var token);
        if(!tokenInHeaders) return null;

        return dbContext.ClientTokens
                .Where(tokenFromList => tokenFromList.Guid == new Guid(token.ToString()))
                .FirstOrDefault();
    }

    public EndpointClient? IsAuthorized(Token token, string? endpointName,
                                        EndpointContext dbContext,
                                        TokenRole tokenRole=TokenRole.Endpoint){
        var foundEndpoint = dbContext.ClientEndpoints.Include(endpointx => endpointx.Token)
                                     .Where(endpointx =>
                                            endpointx.Token.Id == token.Id)
                                     .FirstOrDefault();
        if(foundEndpoint == null) return null;

        return endpointName is null ? 
                                    token.Role == tokenRole ? foundEndpoint :
                                        (token.Role == TokenRole.Admin ? foundEndpoint : null)
                                    :
                                    foundEndpoint.Name.Equals(endpointName) ? foundEndpoint : null;
    }

    /// <summary>
    /// Retrieves a list of EndpointDto objects from EndpointContext.
    /// </summary>
    /// <returns>A list of EndpointDto objects.</returns>
    [HttpGet("/api/endpoints")]
    public List<EndpointDto> Get(EndpointContext dbContext){
        return [.. dbContext.ClientEndpoints.Include(endpoint => endpoint.Logs)
                .Select(x => new EndpointDto(x.Name, x.Logs))];
    }

    /// <summary>Handles the POST request to create a new client endpoint.
    /// Checks if the endpoint already exists, creates a new token and confirmation if not, 
    /// then adds the new client endpoint to the database and returns the result.</summary>
    [HttpPost("/api/endpoint")]
    public ActionResult<string> CreateEndpoint([FromBody] EndpointDto endpoint,
                                                            EndpointContext dbContext){
        var ExistingEndpoint = dbContext.ClientEndpoints
                        .Where(endpointx =>
                               endpointx.Name == endpoint.Name)
                        .FirstOrDefault();
        if(ExistingEndpoint != null) return Conflict(ExistingEndpoint.Name);

        Token token = new(){
            Guid = Guid.NewGuid(),
            IsActive = false
        };
        dbContext.ClientTokens.Add(token);

        Confirmation confirmation = new(){
            Guid = Guid.NewGuid(),
            IsConfirmed = false
        };
        dbContext.ClientEndpointConfirmations.Add(confirmation);

        EndpointClient client = new(){
            Name = endpoint.Name,
            Token = token,
            Confirmation = confirmation
        };

        dbContext.ClientEndpoints.Add(client);
        dbContext.SaveChanges();
        return Ok(new EndpointCreatedDto(token.Guid));
    }

    [HttpPost("/api/endpoint/log")]
    public async Task<ActionResult<string>> AddEndpointLog([FromBody] LogToAddDto log,
                                                            EndpointContext dbContext){
        Token? token = GetTokenFromHeaders(Request.Headers, dbContext);
        if(token == null) return Unauthorized();

        EndpointClient? endpointClient = IsAuthorized(token, log.EndpointName, dbContext);
        if(endpointClient == null) return Unauthorized();

        Log newLog = new(){
            Message = log.Message,
            Time = DateTime.Now
        };
        await dbContext.ClientEndpointLogs.AddAsync(newLog);
        endpointClient.Logs.Add(newLog);
        await dbContext.SaveChangesAsync();
        return Ok(newLog);
    }

    [HttpGet("/api/endpoint/logs")]
    public ActionResult<string> GetEndpointLogs([FromBody] EndpointDto endpoint, EndpointContext dbContext){
        Token? token = GetTokenFromHeaders(Request.Headers, dbContext);
        if(token == null) return Unauthorized();

        EndpointClient? endpointClient = IsAuthorized(token, endpoint.Name, dbContext);
        if(endpointClient == null) return Unauthorized();

        return Ok(dbContext.ClientEndpointLogs.Where(log => log.EndpointClientId == endpointClient.Id).Select(log => new LogDto(log.Message, log.Time)).ToList() ?? []);
    }
}