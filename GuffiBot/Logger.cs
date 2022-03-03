using Discord;
using Discord.WebSocket;
using static System.Guid;

namespace GuffiBot.Log
{
    public abstract class Logger : ILogger
    {
        public string _guid;
        public Logger()
        {

            // extra data to show individual logger instances
            _guid = NewGuid().ToString()[^4..];

        }

        public abstract Task Log(LogMessage message);
        internal abstract SocketTextChannel GetChannel(object channelID);
    }
}
