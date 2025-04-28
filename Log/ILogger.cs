using System.Threading.Tasks;

namespace Aritix.Log;

public interface ILogger
{
    Task Log(string message, FileLogger.LogLevel level);
    void SetMinimumLevel(FileLogger.LogLevel minimumLevel);
}