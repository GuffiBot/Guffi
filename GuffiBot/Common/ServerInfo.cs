using Discord;
using Discord.Interactions;
using GuffiBot.Log;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GuffiBot
{
    // Must use InteractionModuleBase<SocketInteractionContext> for the InteractionService to auto-register the commands
    public class ServerInfo : InteractionModuleBase<SocketInteractionContext>
    {
        public InteractionService Commands { get; set; }
        private static Logger? _logger;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ServerInfo(ConsoleLogger logger)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _logger = logger;
        }


        // Basic slash command. [SlashCommand("name", "description")]
        // Similar to text command creation, and their respective attributes
        [SlashCommand("server", "command a server info!")]
        public async Task ServerAsync()
        {
            EmbedBuilder builder = new EmbedBuilder()
            .WithColor(Color.Blue)
            .WithDescription("**Guild name : **" + Context.Guild.Name + Environment.NewLine + "**Total member : **" + Context.Guild.MemberCount + Environment.NewLine + "**Date Created : **" + Context.Guild.CreatedAt + Environment.NewLine + "**Guild id : **" + Context.Guild.Id + Environment.NewLine + "**Owner id : **" + Context.Guild.OwnerId + Environment.NewLine + "**Owner name : **" + Context.Guild.Owner.Username + "#" + Context.Guild.Owner.Discriminator + Environment.NewLine + "**Total textchannel : **" + Context.Guild.TextChannels.Count + Environment.NewLine + "**Total voice channel : **" + Context.Guild.VoiceChannels.Count)
            .WithThumbnailUrl(Context.Guild.IconUrl)
            .WithFooter($"{DateTime.Now}");
#pragma warning disable CS8602
            await _logger.Log(new LogMessage(LogSeverity.Info, "ServerInfo : ServerInfo", $"User: {Context.User.Username}, Command: server", null));
#pragma warning restore CS8602 

            await ReplyAsync("", false, builder.Build());
        }

    }
}
