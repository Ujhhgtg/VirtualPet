using System.Diagnostics;
using Serilog;
using Serilog.Events;
using Ujhhgtg.Library;
using Ujhhgtg.Library.ExtensionMethods;

namespace VirtualPet.Manager.Cli;

public static class Program
{
    private static void Main(string[] args)
    {
        #if DEBUG
        LogUtils.SetupBasicLogger("./logs/manager-cli-debug.log", LogEventLevel.Debug, RollingInterval.Infinite);
        #else
        LogUtils.SetupBasicLogger("./logs/manager-cli.log", LogEventLevel.Warning, RollingInterval.Day);
        #endif

        if (args.Length == 0)
        {
            Log.Error("[manager-cli] Invalid arguments");
        }

        switch (args[0])
        {
            case "convert-lps-to-json":
                Debug.Assert(args.Length == 3);
                
                var inputPath = args[1];
                var outputPath = args[2];
                
                var inputPathFiles = Directory.EnumerateFiles(inputPath, "*.lps", SearchOption.AllDirectories).AsPaths();
                foreach (var inputFile in inputPathFiles)
                {
                    var lines = FileUtils.ReadText(inputFile)!.Replace(":\n:", "").Replace(":\n|", "\n").Replace(@"\n", "").Replace("\n", "").Split('\n');

                    #region Parses LPS files

                    foreach (var line in lines)
                    {
                        var pairs = line.Split(":|");
                        
                        foreach (var pair in pairs)
                        {
                            var splitPair = pair.Split('#');
                            var key = splitPair[0];
                            var value = splitPair[1];
                        }
                    }
                    #endregion
                }
        }
    }

    // private static class Guard
    // {
    //     public static void CompareInts(Comparison comparison, int target1, int target2, string message = "Ass")
    //     {
    //         switch (comparison)
    //         {
    //             case Comparison.Equals:
    //                 if (target1 != target2)
    //                     throw new ArgumentException(message);
    //                 break;
    //             case Comparison.BiggerThan:
    //                 if (target1 <= target2)
    //                     throw new ArgumentException(message);
    //                 break;
    //             case Comparison.SmallerThan:
    //                 if (target1 >= target2)
    //                     throw new ArgumentException(message);
    //                 break;
    //             default:
    //                 throw new ArgumentOutOfRangeException(nameof(comparison), comparison, null);
    //         }
    //     }
    //
    //     public enum Comparison
    //     {
    //         BiggerThan,
    //         Equals,
    //         SmallerThan
    //     }
    // }
}