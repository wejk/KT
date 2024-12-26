using Microsoft.Extensions.Caching.Memory;
using MyLibrary;
using MyLibrary.Data;

namespace WebApiCore.Services;

public sealed class HelperService : IHelperService
{
    private readonly IMemoryCache _memoryCache;
    private readonly IRepo _repo;

    public HelperService(IMemoryCache memoryCache, IRepo repo)
    {
        _memoryCache = memoryCache;
        _repo = repo;
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

    public Dictionary<string, object> GetSamples()
    {
        return _repo.GetSamples();
    }
}
