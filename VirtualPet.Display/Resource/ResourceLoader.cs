using Ujhhgtg.Library;
using VirtualPet.Display.Resource.Models;

namespace VirtualPet.Display.Resource;

// ReSharper disable once InconsistentNaming
public interface ResourceLoader
{
    public PetResource LoadPet(PathObject path);
    public AnimationResource LoadAnimation(PathObject path);
}
