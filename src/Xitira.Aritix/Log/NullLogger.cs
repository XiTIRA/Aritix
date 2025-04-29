using System.Threading.Tasks;

namespace Xitira.Aritix.Log;

public class NullLogger : ILogger
{
    public async Task Log(string message, FileLogger.LogLevel level)
    {
        
    }

    public void SetMinimumLevel(FileLogger.LogLevel minimumLevel)
    {
        
    }
}