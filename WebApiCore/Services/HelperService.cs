using Microsoft.Extensions.Caching.Memory;
using MyLibrary;
using MyLibrary.Data;

namespace WebApiCore.Services;

public sealed class HelperService : IHelperService
{
    private readonly IMemoryCache _memoryCache;
    private readonly IRepo _repo;
    private readonly ILogger<HelperService> _logger;
    public HelperService(IMemoryCache memoryCache, IRepo repo, ILogger<HelperService> logger)
    {
        _memoryCache = memoryCache;
        _repo = repo;
        _logger = logger;
    }

    public string[] Get(int count, Categories key)
    {
        if (_memoryCache.TryGetValue(key, out string[] items) && items is not null)
        {
            return StringHelper.RandomWords(items, count).ToArray();
        }
        else
        {
            items = GetByCategory(key.ToString(), count);
            _memoryCache.Set(key, items);
            return StringHelper.RandomWords(items, count).ToArray();
        }
    }

    private string[] GetByCategory(string key, int count)
    {
        try
        {
            var items = _repo.Get(key, count).ToArray();
            return items;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting data from repo");
            throw;
        }
    }

    public Dictionary<string, IEnumerable<string>> GetSamples()
    {
        return _repo.GetSamples();
    }
}
