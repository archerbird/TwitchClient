using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using TwitchClient.Application.Users;
using System.Text.Json;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var config = new ConfigurationBuilder()
    //.SetBasePath(AppContext.BaseDirectory)
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}", true, true)
    .AddUserSecrets<Program>()
    .Build();

using IHost host = Host.CreateDefaultBuilder()
.ConfigureServices(services =>
services.AddInfrastructure(config))
.Build();

var userService = host.Services.GetRequiredService<IUsersService>();
var user = await userService.GetUserByLogin("ArcherBird", CancellationToken.None);

Console.WriteLine(JsonSerializer.Serialize(user, new JsonSerializerOptions { WriteIndented = true }));

//await host.RunAsync();