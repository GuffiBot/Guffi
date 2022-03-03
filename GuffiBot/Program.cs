using Discord.Interactions;
using Discord.WebSocket;
using GuffiBot.Log;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GuffiBot;

namespace GuffiBot
{
    public class program
    {
        private DiscordSocketClient? _client;

        // Program entry point
        public static Task Main(string[] args) => new program().MainAsync();

        public async Task MainAsync()
        {
            var config = new ConfigurationBuilder()
            .AddEnvironmentVariables(prefix: "&")
            // this will be used more later on
            .SetBasePath(AppContext.BaseDirectory)
            // I chose using YML files for my config data as I am familiar with them
            .AddYamlFile("config.yml")
            .Build();

            using IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
            services
            // Add the configuration to the registered services
            .AddSingleton(config)
            // Add the DiscordSocketClient, along with specifying the GatewayIntents and user caching
            .AddSingleton(x => new DiscordSocketClient(new DiscordSocketConfig
            {
                GatewayIntents = Discord.GatewayIntents.AllUnprivileged,
                AlwaysDownloadUsers = true,
            }))
            // Adding console logging
            .AddTransient<ConsoleLogger>()
            // Used for slash commands and their registration with Discord
            .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
            // Required to subscribe to the various client events used in conjunction with Interactions
            .AddSingleton<InteractionHandler>())

            .Build();

            await RunAsync(host);
        }

        public async Task RunAsync(IHost host)
        {
            Console.WriteLine(@"

   _____          __   __  _  ____          _   
  / ____|        / _| / _|(_)|  _ \        | |  
 | |  __  _   _ | |_ | |_  _ | |_) |  ___  | |_ 
 | | |_ || | | ||  _||  _|| ||  _ <  / _ \ | __|
 | |__| || |_| || |  | |  | || |_) || (_) || |_ 
  \_____| \__,_||_|  |_|  |_||____/  \___/  \__|
                                                
                                                           
");
            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            var commands = provider.GetRequiredService<InteractionService>();
            _client = provider.GetRequiredService<DiscordSocketClient>();
            var config = provider.GetRequiredService<IConfigurationRoot>();

            await provider.GetRequiredService<InteractionHandler>().InitializeAsync();

            // Subscribe to client log events
            _client.Log += _ => provider.GetRequiredService<ConsoleLogger>().Log(_);
            // Subscribe to slash command log events
            commands.Log += _ => provider.GetRequiredService<ConsoleLogger>().Log(_);

            _client.Ready += async () =>
            {
                    await commands.RegisterCommandsGloballyAsync();
            };
            await _client.LoginAsync(Discord.TokenType.Bot, config["tokens:discord"]);
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        static bool IsDebug()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }
}