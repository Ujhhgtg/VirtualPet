using System.Collections.Generic;

namespace VirtualPet.Display.Resource.Models;

public class PetResource
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public int Version { get; set; }
    public List<string> Authors { get; set; } = [];
    public int AnimationCount { get; set; }
}