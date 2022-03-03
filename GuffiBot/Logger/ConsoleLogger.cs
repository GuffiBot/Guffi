using Discord;
using Discord.WebSocket;

namespace GuffiBot.Log
{
    public class ConsoleLogger : Logger
    {
        // Override Log method from ILogger, passing message to LogToConsoleAsync()
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public override async Task Log(LogMessage message)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {

            // Using Task.Run() in case there are any long running actions, to prevent blocking gateway
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Task.Run(() => LogToConsoleAsync(this, message));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task LogToConsoleAsync<T>(T logger, LogMessage message) where T : ILogger
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            Console.WriteLine($"guid:{_guid} : " + message);
        }

        internal override SocketTextChannel GetChannel(object channelID)
        {
            throw new NotImplementedException();
        }
    }
}