using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Sdl = Xitira.Aritix.Extensions.Sdl;

namespace Xitira.Aritix.Log;

public class FileLogger : ILogger
{
    private string _logFile;
    private readonly Lock _lock = new();
    
    private LogLevel _minLevel;

    public FileLogger(string organization, string application, LogLevel minimumLevel)
    {
        _logFile = Path.Combine(Sdl.GetPrefPath(organization, application), $"{application}.log");
        _minLevel = minimumLevel;
    }

    public void SetMinimumLevel(LogLevel minimumLevel)
    {
        _minLevel = minimumLevel;
    }

    public async Task Log(string message, LogLevel level)
    {
        await Task.Run(() =>
        {
            lock (_lock)
            {
                using StreamWriter writer = new StreamWriter(_logFile, true);

                string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";

                writer.WriteLine(logMessage);
                Console.WriteLine(logMessage);
            }
        });
    }

    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error,
        Fatal
    }
}