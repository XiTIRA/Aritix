using System.Reflection;
using System.Runtime.InteropServices;

namespace Xitira.Aritix.Sdl;

public static partial class Sdl
{
    static Sdl()
    {
        NativeLibrary.SetDllImportResolver(Assembly.GetExecutingAssembly(), DllImportResolver);
    }
    
    private const string SDL_Library = "SDL2";
    private const string SDL_Library_W64 = "SDL2";
    private const string SDL_Library_Linux = "libSDL2-2.0.so.0";

    [LibraryImport(SDL_Library, StringMarshalling = StringMarshalling.Utf8)]
    private static partial int SDL_GetPowerInfo(out int secs, out int pct);

    [LibraryImport(SDL_Library, StringMarshalling = StringMarshalling.Utf8)]
    private static partial void SDL_SetWindowMinimumSize(IntPtr window, int minW, int minH);

    public static void SetWindowMinimumSize(IntPtr window, int minW, int minH)
    {
        SDL_SetWindowMinimumSize(window, minW, minH);
    }

    public static SdlPowerInfo GetPowerInfo()
    {
        int state = SDL_GetPowerInfo(out int secs, out int pct);
        return new SdlPowerInfo()
        {
            Seconds = secs,
            Percent = pct,
            State = (SdlPowerState)state
        };
    }


    [LibraryImport(SDL_Library, StringMarshalling = StringMarshalling.Utf8)]
    private static partial IntPtr SDL_GetPrefPath(string org, string app);

    public static string GetPrefPath(string org, string app)
    {
        IntPtr result = SDL_GetPrefPath(org, app);
        string path = Marshal.PtrToStringAnsi(result);
        Marshal.FreeCoTaskMem(result);
        return path;
    }

    private static IntPtr DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        if (libraryName == SDL_Library)
        {
            
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return NativeLibrary.Load(SDL_Library_Linux, assembly, searchPath);
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return NativeLibrary.Load(SDL_Library_W64, assembly, searchPath);
            }
        }

        // Return IntPtr.Zero if the library name does not match
        return IntPtr.Zero;
    }
}

public struct SdlPowerInfo
{
    public int Seconds;
    public int Percent;
    public SdlPowerState State;

    public override string ToString()
    {
        return $"Seconds: {Seconds}, Percent: {Percent}, State: {State.ToString()}";
    }
}

public enum SdlPowerState
{
    Unknown,
    OnBattery,
    NoBattery,
    Charging,
    Charged
}