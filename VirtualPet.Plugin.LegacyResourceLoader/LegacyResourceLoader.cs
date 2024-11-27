using LinePutScript;
using Serilog;
using Ujhhgtg.Library;
using Ujhhgtg.Library.ExtensionMethods;
using VirtualPet.Display.Resource;
using VirtualPet.Display.Resource.Models;

namespace VirtualPet.Plugin.LegacyResourceLoader;

public class LegacyResourceLoader : ResourceLoader
{
    public List<PetResource> LoadPets(PathObject path)
    {
        var modDirs = (path / "legacy").AsDirectory().EnumerateDirectories().AsPaths().ToList();
        
        if (!(path / "legacy").IsDirectory || modDirs.Count == 0)
            return [];

        foreach (var modDir in modDirs)
        {
            if (modDir.FileName != "0000_core")
            {
                Log.Error("[legacy-resource-loader] Resource in {Path} is not 'Core' mod; skipping...", modDir);
                continue;
            }

            var modInfoPath = modDir / "info.lps";
            if (!modInfoPath.IsFile)
            {
                Log.Warning("[legacy-resource-loader] Resource info file {Path} does not exist", modInfoPath);
            }

            if ((modDir / "theme").IsDirectory)
            {
                foreach (var themeInfoLpsPath in (modDir / "theme").AsDirectory().EnumerateFiles("*.lps"))
                {
                    var theme = new ThemeLegacyResource();
                }
            }
            
            foreach (var petInfoLpsPath in (modDir / "pets").AsDirectory().EnumerateFiles("*.lps").AsPaths())
            {
                var petId = petInfoLpsPath.FileName;
                var petInfoDoc = new LpsDocument(FileUtils.ReadText(petInfoLpsPath)!);
                
            }
            
            
        }
    }
    
    public PetResource LoadPet(PathObject path)
    {
        
        
        
    }

    public AnimationResource LoadAnimation(PathObject path)
    {
        throw new NotImplementedException();
    }
}