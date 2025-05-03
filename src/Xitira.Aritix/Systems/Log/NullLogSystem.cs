using System.Threading.Tasks;
using Xitira.Aritix.Annex;

namespace Xitira.Aritix.Systems.Log;

public class NullLogSystem : ILogSystem
{
    public async Task Log(string message, LogLevels level)
    {
        
    }

    public async Task LogDebug(string message)
    {
        _ = Log(message, LogLevels.Debug);
    }

    public async Task LogInfo(string message)
    {
        _ = Log(message, LogLevels.Info);
    }

    public async Task LogWarning(string message)
    {
        _ = Log(message, LogLevels.Warning);
    }

    public async Task LogError(string message)
    {
       _ = Log(message, LogLevels.Error);
    }

    public async Task LogFatal(string message)
    {
        _ = Log(message, LogLevels.Fatal);
    }

    public void SetMinimumLevel(LogLevels minimumLevel)
    {
        return;
    }
}