using Central1zedCSharp.BackgroundWorkers;
using Central1zedCSharp.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connString = builder.Configuration.GetConnectionString("EndpointClients");
builder.Services.AddSqlite<EndpointContext>(connString);

builder.Services.AddHostedService<PoolerBgService>();

var nameOfPolicyCORS  = "MyAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: nameOfPolicyCORS ,
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:3000");
                      });
});
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.UseCors(nameOfPolicyCORS);

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

app.Run();