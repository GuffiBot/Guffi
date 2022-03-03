using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Interactions;
using GuffiBot.Log;
using System.Data;

namespace GuffiBot
{
    public class MathCmd : InteractionModuleBase<SocketInteractionContext>
    {
        public InteractionService Commands { get; set; }
        private static Logger? _logger;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public MathCmd(ConsoleLogger logger)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _logger = logger;
        }

        [SlashCommand("math", "command math")]
        public async Task MathAsync([Remainder] string math)
        {
            var dt = new DataTable();
            var result = dt.Compute(math, null);

            await ReplyAsync($":abacus: `Result:` **{result}**");
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            await _logger.Log(new LogMessage(LogSeverity.Info, "MathModule : Math", $"User: {Context.User.Username}, Command: math", null));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        }
    }
}

