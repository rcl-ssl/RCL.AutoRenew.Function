#nullable disable

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IConfiguration configuration = null;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration(builder =>
        {
            builder.AddJsonFile("local.settings.json", true, true);
            builder.AddEnvironmentVariables();
            configuration = builder.Build();
        })
        .ConfigureServices(services =>
        {
            services.AddAuthTokenService(options => configuration.Bind("Auth", options));
            services.AddApiRequestService(options => configuration.Bind("Api", options));
        })
    .Build();

host.Run();