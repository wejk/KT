using Microsoft.Extensions.Caching.Memory;
using MyLibrary;
namespace WebApiCore.Services;

public sealed class HelperService : IHelperService
{
    private readonly IMemoryCache _memoryCache;

    public HelperService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public string[] Get(int count)
    {
        var key = "mykey";

        if (_memoryCache.TryGetValue(key, out string[] items))
        {
            return StringHelper.RandomWords(items, count).ToArray();
        }
        else
        {
            items = Constants.Vegetables;
            _memoryCache.Set(key, items);
            return StringHelper.RandomWords(items, count).ToArray();
        }
    }
}
