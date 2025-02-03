using Serilog;

namespace Hogarth.UserManagement.API.Extensions
{
    public static class SerilogExtensions
    {
        public static void AddSerilogLogging(this IHostBuilder hostBuilder)
        {
            string logDirectory = "logs";
            string timestamp = DateTime.Now.ToString("dd-MM-yyyy##hh-mmtt");
            string logFileName = $"{timestamp}_Hogarth.UserManagement.Logging.txt";
            string logFilePath = Path.Combine(logDirectory, logFileName);

            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Infinite)
                .CreateLogger();

            hostBuilder.UseSerilog();
            DeleteOldLogs(logDirectory, 7);
        }

        private static void DeleteOldLogs(string logDirectory, int daysToKeep)
        {
            try
            {
                var logFiles = new DirectoryInfo(logDirectory).GetFiles()
                    .OrderByDescending(f => f.CreationTime)
                    .ToList();

                if (logFiles.Count > daysToKeep)
                {
                    foreach (var file in logFiles.Skip(daysToKeep))
                    {
                        file.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Error while deleting old log files: {ex.Message}");
            }
        }
    }
}
