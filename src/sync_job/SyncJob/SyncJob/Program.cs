using SyncJob;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

try
{
    var services = new ServiceCollection();
    string postgresConnection = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION");
    services.AddDbContextPool<AppDbContext>(options => options.UseNpgsql(postgresConnection));

    var serviceProvider = services.BuildServiceProvider();
    var mqttClientHandler = new MqttClientHandler(serviceProvider);
    
    await mqttClientHandler.InitClient();    
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    throw;
}


