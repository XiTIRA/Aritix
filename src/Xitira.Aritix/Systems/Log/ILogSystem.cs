using System.Threading.Tasks;

namespace Xitira.Aritix.Annex;

public interface ILogSystem
{
    Task Log(string message, LogLevels level);
    Task LogDebug(string message);
    Task LogInfo(string message);
    Task LogWarning(string message);
    Task LogError(string message);
    Task LogFatal(string message);
    void SetMinimumLevel(LogLevels minimumLevel);
}