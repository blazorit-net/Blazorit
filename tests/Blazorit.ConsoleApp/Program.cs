// See https://aka.ms/new-console-template for more information
using Blazorit.ConsoleApp.Services.ECommerce;
using Blazorit.Infrastructure.DBStorages.BlazoritDB.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog.Events;
using Serilog;


//################################################################
//  ##################--Logging Wrapper--#######################
//      https://github.com/serilog/serilog-extensions-hosting
//################################################################
Log.Logger = new LoggerConfiguration() //logger settings
            .MinimumLevel.Debug()
            .MinimumLevel.Verbose()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

//Start Host in try-catch-finally
try {
    Log.Information("Starting host");
    await Host.CreateDefaultBuilder(args)
        .UseSerilog()
        .ConfigureServices((hostContext, services) => {
            //services.AddLogging();
            services.AddHostedService<ConsoleHostedService>();
            services.AddDbContextFactory<BlazoritContext>(options =>
                options.UseNpgsql(hostContext.Configuration.GetConnectionString("DefaultConnection")));

            //################################################################
            //  ######################--ECOMMERCE--#########################
            //################################################################
            services.AddScoped<Blazorit.Infrastructure.Repositories.Abstract.ECommerce.IECommerceRepository, Blazorit.Infrastructure.Repositories.Concrete.ECommerce.ECommerceRepository>();
            services.AddScoped<Blazorit.Core.Services.Abstract.ECommerce.Domain.Data.IDataService, Blazorit.Core.Services.Concrete.ECommerce.Domain.Data.DataService>();
            services.AddScoped<Blazorit.Server.Services.Abstract.ECommerce.Domain.Data.IDataService, Blazorit.Server.Services.Concrete.ECommerce.Domain.Data.DataService>();
            //################################################################
            //  ############################################################
            //################################################################
        })
        .RunConsoleAsync();
    return 0;
} catch (Exception ex) {
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
} finally {
    Log.CloseAndFlush();
}
//################################################################
//  ############################################################
//################################################################


/***
await Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging => {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .ConfigureServices((hostContext, services) => {
        services.AddLogging();
        services.AddHostedService<ConsoleHostedService>();
        services.AddDbContextFactory<BlazoritContext>(options => 
            options.UseNpgsql(hostContext.Configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<Blazorit.Infrastructure.Repositories.Abstract.ECommerce.IECommerceRepository, Blazorit.Infrastructure.Repositories.Concrete.ECommerce.ECommerceRepository>();
    })
    .RunConsoleAsync();
//.Build();
//var host = builder.Build();
//await host.RunAsync();

Console.WriteLine("The End");
***/
