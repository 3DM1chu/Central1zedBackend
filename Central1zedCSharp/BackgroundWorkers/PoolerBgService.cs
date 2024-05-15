namespace Central1zedCSharp.BackgroundWorkers
{
    public class PoolerBgService(IServiceProvider serviceProvider) : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested){
                /*using var scope = _serviceProvider.CreateScope();
                var ctx = scope.ServiceProvider.GetRequiredService<EndpointContext>();
                var endpoints = ctx.ClientEndpoints.ToList();
                foreach(var endpoint in endpoints){
                    endpoint.Name += "1";
                }
                //ctx.SaveChanges();

                */
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}