using Teko.Resources;

namespace AIWorld.Registry;

public class Domain<T> : IDomain where T : IResource
{
    private Dictionary<string, T> _resources = new();
    
    public void Load(ResourcesLoader loader, string file)
        => _resources.Add(Path.GetFileName(file).Split(".")[0], loader.LoadResource<T>(file)!);

    public T? Get(string name)
    {
        _resources.TryGetValue(name, out var result);
        return result;
    }
}