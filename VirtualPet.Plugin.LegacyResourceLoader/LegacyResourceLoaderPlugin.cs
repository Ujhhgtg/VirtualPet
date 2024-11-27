﻿using VirtualPet.Display;

namespace VirtualPet.Plugin.LegacyResourceLoader;

public class LegacyResourceLoaderPlugin : Display.Plugin.Plugin
{
    public override void OnLoad(MainWindow window)
    {
        window.ResourceManager.RegisterResourceLoader(typeof(LegacyResourceLoader));
    }

    public override void OnDisable(MainWindow window)
    {
        throw new NotImplementedException();
    }

    public override void OnEnable(MainWindow window)
    {
        throw new NotImplementedException();
    }
}