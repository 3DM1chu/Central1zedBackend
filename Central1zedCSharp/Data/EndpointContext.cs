using Microsoft.EntityFrameworkCore;
using Central1zedCSharp.Entities;

namespace Central1zedCSharp.Data;

public class EndpointContext(DbContextOptions<EndpointContext> options) : DbContext(options){
    public DbSet<EndpointClient> ClientEndpoints => Set<EndpointClient>();
    public DbSet<Log> ClientEndpointLogs => Set<Log>();
    public DbSet<Token> ClientTokens => Set<Token>();
    public DbSet<Confirmation> ClientEndpointConfirmations => Set<Confirmation>();
}