using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Interactions;
using GuffiBot.Log;

namespace GuffiBot
{
    public class PingCmd : InteractionModuleBase<SocketInteractionContext>
    {
        public InteractionService Commands { get; set; }
        private static Logger? _logger;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public PingCmd(ConsoleLogger logger)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _logger = logger;
        }

        [SlashCommand("ping", "command add role")]
        public async Task PingAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle($"Pong! :ping_pong: - {Context.Client.Latency}ms")
                .WithColor(Color.Blue);

            await ReplyAsync("", false, builder.Build());
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            await _logger.Log(new LogMessage(LogSeverity.Info, "PingModule : Ping", $"User: {Context.User.Username}, Command: ping", null));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        }
    }
}
