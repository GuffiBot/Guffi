using Discord;

namespace GuffiBot.Log
{
    public interface     ILogger
    {

        // Establish required method for all Loggers to implement
        public Task Log(LogMessage message);

    }
}
