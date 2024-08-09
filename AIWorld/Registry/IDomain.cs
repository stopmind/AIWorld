using Teko.Resources;

namespace AIWorld.Registry;

public interface IDomain
{
    void Load(ResourcesLoader loader, string file);
}