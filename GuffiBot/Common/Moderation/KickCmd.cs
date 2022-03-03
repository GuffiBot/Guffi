using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Interactions;
using GuffiBot.Log;

namespace GuffiBot
{
    public class KickCmd : InteractionModuleBase<SocketInteractionContext>
    {
        public InteractionService Commands { get; set; }
        private static Logger? _logger;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public KickCmd(ConsoleLogger logger)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _logger = logger;
        }

        [SlashCommand("kick", "command kick")]
        [Discord.Commands.RequireUserPermission(GuildPermission.KickMembers, ErrorMessage = ":lock: You don't have the permission")]
        public async Task KickMember(IGuildUser? user = null, [Remainder] string? reason = null)
        {
            if (user == null)
            {
                await ReplyAsync("Please specify a user!");
                return;
            }
            if (reason == null) reason = "Not specified!";
            await user.KickAsync(reason);

            var EmbedBuilder = new EmbedBuilder()
                .WithColor(Color.Red)
                .WithDescription($":shield: Moderator\n:white_check_mark: {user.Mention} was kicked\n**Reason** {reason}")
                .WithFooter(footer =>
                {
                    footer
                    .WithText("User Kick Log")
                    .WithIconUrl("https://i.imgur.com/6Bi17B3.png");
                });
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            await _logger.Log(new LogMessage(LogSeverity.Info, "KickModule : Kick", $"User: {Context.User.Username}, Command: kick", null));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}
