using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Serilog;
using VirtualPet.Display.Plugin;
using VirtualPet.Display.Resource;

namespace VirtualPet.Display;

public partial class MainWindow : Window
{
    public PluginManager PluginManager { get; }
    public ResourceManager ResourceManager { get; }
    
    public MainWindow()
    {
        PluginManager = new PluginManager(this);
        ResourceManager = new ResourceManager(this);
        
        Log.Verbose("[ui] Initializing ui...");
        InitializeComponent();
        Position = new PixelPoint(0, 0);
        
        Log.Verbose("[ui] Loading plugins...");
        PluginManager.LoadPlugins();
        
        Log.Verbose("[ui] Window loaded");
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        Log.Verbose("[ui] Closing window...");
        Close();
    }
}