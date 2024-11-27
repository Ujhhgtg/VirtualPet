using Avalonia;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Serilog;
using Serilog.Events;
using Ujhhgtg.Library;

namespace VirtualPet.Display;

internal static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        #if DEBUG
        LogUtils.SetupBasicLogger("./logs/display-debug.log", LogEventLevel.Debug, RollingInterval.Infinite);
        #else
        LogUtils.SetupBasicLogger("./logs/display.log", LogEventLevel.Warning, RollingInterval.Day);
        #endif
        
        ProcessUtils.EnsureWorkingDirectory();
        
        Log.Verbose("[main] Starting VirtualPet display component...");
        Log.Verbose("[main] Current operating system is {OS}", RuntimeInformation.RuntimeIdentifier);

        if (OperatingSystem.IsLinux())
        {
            // i performed a 'paru tiling' to find all tiling window managers
            if ((Environment.GetEnvironmentVariable("XDG_CURRENT_DESKTOP") ?? "").ToLower() is "hyprland" or "i3" or "sway"
                or "bspwm" or "stumpwm" or "river" or "qtile" or "niri" or "notion" or "leftwm")
            {
                Log.Warning("[main] Tiling window manager is detected; VirtualPet may not work as expected");
                Log.Warning("[main] Modify your window manager configuration to allow VirtualPet to work properly");

                if (Environment.GetEnvironmentVariable("XDG_CURRENT_DESKTOP")! == "Hyprland")
                {
                    Log.Warning("[main] Adding configuration for VirtualPet to your Hyprland configuration...");
                    
                    var configPath = KnownPaths.Config/ "hypr" / "hyprland.conf";
                    if (configPath.IsFile && !(FileUtils.ReadText(configPath) ?? "").Contains("VirtualPet Configuration"))
                    {
                        using var fileWriter = new StreamWriter(configPath, Encoding.Default, new FileStreamOptions { Mode = FileMode.Append, Access = FileAccess.Write});
                        fileWriter.WriteLine("# BEGIN VirtualPet Configuration");
                        fileWriter.WriteLine("windowrule = float, title:^(VirtualPet Display)$");
                        fileWriter.WriteLine("windowrule = noblur, title:^(VirtualPet Display)$");
                        fileWriter.WriteLine("windowrule = noshadow, title:^(VirtualPet Display)$");
                        fileWriter.WriteLine("windowrule = noinitialfocus, title:^(VirtualPet Display)$");
                        fileWriter.WriteLine("# END VirtualPet Configuration");
                    }
                }
            }
        }
        
        // Log.Verbose("[main] Starting pipe server in second thread...");
        // PipeServerThread = new Thread(RunPipeServerThread);
        // PipeServerThread.Start();
        
        Log.Verbose("[main] Entering Avalonia UI lifetime...");
        
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    private static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithSystemFontSource(new Uri("avares://GoogleFonts/Assets/Fonts#Nunito"))
            .LogToTrace();
    }
}