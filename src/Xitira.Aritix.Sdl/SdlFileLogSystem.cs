using Xitira.Aritix.Annex;

namespace Xitira.Aritix.Sdl;

public class SdlFileLogSystem : ILogSystem
{
    private string _logFile;
    private readonly Lock _lock = new();
    
    private LogLevels _minLevel;

    public SdlFileLogSystem(string organization, string application, LogLevels minimumLevel)
    {
        _logFile = Path.Combine(Sdl.GetPrefPath(organization, application), $"{application}.log");
        _minLevel = minimumLevel;
    }

    public async Task LogFatal(string message)
    {
        await Log(message, LogLevels.Fatal);
    }

    public void SetMinimumLevel(LogLevels minimumLevel)
    {
        _minLevel = minimumLevel;
    }

    public async Task Log(string message, LogLevels level)
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

    public async Task LogDebug(string message)
    {
        await Log(message, LogLevels.Debug);
    }

    public async Task LogInfo(string message)
    {
        await Log(message, LogLevels.Info);
    }

    public async Task LogWarning(string message)
    {
        await Log(message, LogLevels.Warning);
    }

    public async Task LogError(string message)
    {
        await Log(message, LogLevels.Error);
    }
}