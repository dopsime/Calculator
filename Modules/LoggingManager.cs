using NLog;

namespace Calculator.Modules
{
    public static class LoggingManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public static bool IsLoggingEnabled { get; set; } = false;

        public static void LogInfo(string message)
        {
            if (IsLoggingEnabled)
            {
                Logger.Info(message);
            }
        }

        public static void LogError(string message)
        {
            if (IsLoggingEnabled)
            {
                Logger.Error(message);
            }
        }

        public static void LogDebug(string message)
        {
            if (IsLoggingEnabled)
            {
                Logger.Debug(message);
            }
        }
    }
}
