using Discord;
using Discord.Interactions;
using GuffiBot.Log;

namespace GuffiBot
{
    public class HelpCmd : InteractionModuleBase<SocketInteractionContext>
    {
        public InteractionService Commands { get; set; }
        private static Logger? _logger;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public HelpCmd(ConsoleLogger logger)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _logger = logger;
        }

        [SlashCommand("help", "command help")]

        public async Task HelpAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.AddField("Guffi Command Help", "Bot created by @Zont1k#5100")
                .AddField(":books: Commands",
                "**help** | Brings you here" +
                "\n**serverinfo** | See info on the current server" +
                "\n**userinfo** | See info on a certain user" +
                "\n**ping** | Checks the delay between you and the bot" +
                "\n**bean** | Bean a member" +
                "\n**neko** | Post a random neko pic (you need attach files perm in that channel)")
                .AddField(":shield: Staff Commands",
                "\n**ban** | Ban a member (requires server ban permission)" +
                "\n**purge** | Purge x messages, where x is 2-100" +
                "\n**role add** | usage: n!role add @user role (put role in quotations)" +
                "\n**role remove** | usage: n!role remove @user role (put role in quotations)")
                .AddField(":lock: NSFW Commands",
                "\n**lewd** | Post a random NSFW Neko (requires NSFW channel)" +
                "\n**pussy** | Post a random pussy pic (requires NSFW channel)" +
                "\n**boobs** | Post a random boob pic (requires NSFW channel)" +
                "\n**nekogif** | Post a random NSFW Neko Gif (requires NSFW channel)" +
                "\n**lesbian** | Post a random NSFW lesbian pic (requires NSFW channel)")
                .WithColor(Color.Blue)
                .WithThumbnailUrl(Context.Guild.IconUrl);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            await _logger.Log(new LogMessage(LogSeverity.Info, "HelpModule : Help", $"User: {Context.User.Username}, Command: help", null));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            await ReplyAsync("", false, builder.Build());
        }
    }
}

