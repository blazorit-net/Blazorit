// See https://aka.ms/new-console-template for more information
using Blazorit.ConsoleApp.Services.ECommerce;
using Blazorit.Infrastructure.DBStorages.BlazoritDB.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Hello, World!");

await Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) => {
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