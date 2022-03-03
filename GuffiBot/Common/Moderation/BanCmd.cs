using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Interactions;
using GuffiBot.Log;

namespace GuffiBot
{
    public class BanCmd : InteractionModuleBase<SocketInteractionContext>
    {
        public InteractionService Commands { get; set; }
        private static Logger? _logger;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public BanCmd(ConsoleLogger logger)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _logger = logger;
        }

        [SlashCommand("add role", "command add role")]
        [Discord.Commands.RequireUserPermission(GuildPermission.BanMembers, ErrorMessage = ":lock: You don't have the permission")]
        public async Task BanMember(IGuildUser? user = null, [Remainder] string? reason = null)
        {
            if (user == null)
            {
                await ReplyAsync("Please specify a user!");
                return;
            }
            if (reason == null) reason = "Not specified!";
            await Context.Guild.AddBanAsync(user, 1, reason);

            var EmbedBuilder = new EmbedBuilder()
                .WithColor(Color.Red)
                .WithDescription($":white_check_mark: {user.Mention} was banned\n**Reason** {reason}")
                .WithFooter(footer =>
                {
                    footer
                    .WithText("User Ban Log")
                    .WithIconUrl("https://i.imgur.com/6Bi17B3.png");
                });
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            await _logger.Log(new LogMessage(LogSeverity.Info, "BanModule : Ban", $"User: {Context.User.Username}, Command: ban", null));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}

