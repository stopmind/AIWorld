using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Teko.Resources;

namespace AIWorld.Registry;

public class LoadConfig : IResource
{
    public readonly Dictionary<string, string[]> DomainsLoadPaths;
    
    public static dynamic Load(ResourcesLoader loader, Stream stream)
    {
        var reader = new StreamReader(stream);
        var data = JsonConvert.DeserializeObject<JObject>(reader.ReadToEnd())!;
        reader.Close();
        
        return new LoadConfig(data.Value<Dictionary<string, string[]>>()!);
    }

    private LoadConfig(Dictionary<string, string[]> domainsLoadPaths)
    {
        DomainsLoadPaths = domainsLoadPaths;
    }
}