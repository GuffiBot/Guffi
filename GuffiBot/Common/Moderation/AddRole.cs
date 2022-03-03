using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Interactions;
using GuffiBot.Log;

namespace GuffiBot
{
    public class AddRole : InteractionModuleBase<SocketInteractionContext>
    {
        public InteractionService Commands { get; set; }
        private static Logger? _logger;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public AddRole(ConsoleLogger logger)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _logger = logger;
        }

        [SlashCommand("add role", "command add role")]
        [Discord.Commands.RequireUserPermission(GuildPermission.ManageRoles)]

        public async Task AddRoleAsync(SocketGuildUser user, string role)
        {
            var Role = Context.Guild.Roles.FirstOrDefault(x => x.Name.ToString() == $"{role}");
            await user.AddRoleAsync(Role);
            await ReplyAsync($"Role given to {user}: {Role}");
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            await _logger.Log(new LogMessage(LogSeverity.Info, "AddRoleModule : AddRole", $"User: {Context.User.Username}, Command: add role", null));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        [SlashCommand("remove role", "command remove role")]
        [Discord.Commands.RequireUserPermission(GuildPermission.ManageRoles)]

        public async Task RemoveRole(SocketGuildUser user, string role)
        {
            var Role = Context.Guild.Roles.FirstOrDefault(x => x.Name.ToString() == $"{role}");
            await user.RemoveRoleAsync(Role);
            await ReplyAsync($"Role removed from {user}: {Role}");
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            await _logger.Log(new LogMessage(LogSeverity.Info, "RemoveRoleModule : RemoveRole", $"User: {Context.User.Username}, Command: remove role", null));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}
