using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MyLibrary.Data;
public class JsonRepo : IRepo
{
    private Dictionary<string, object> _samples;

    private readonly ILogger<JsonRepo> _logger;
    public JsonRepo(ILogger<JsonRepo> logger)
    {
        _logger = logger;
        _samples = Read();
    }

    internal Dictionary<string, object> Read()
    {
        try
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data.json");
            using StreamReader r = new StreamReader(path);
            string json = r.ReadToEnd();
            var samples = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
            return samples is null ? throw new Exception("Error reading data.json") : samples;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading data.json");
            throw;
        }
    }

    public Dictionary<string, object> GetSamples()
    {
        return _samples;
    }

    public void Add(string key, object value)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException("Key cannot be null or empty");
        }
        if (value == null)
        {
            throw new ArgumentNullException("Value cannot be null");
        }
        if (_samples.ContainsKey(key))
        {
            throw new ArgumentException("Key already exists");
        }
        _samples.Add(key, value);
    }

    public void Update(string key, object value)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException("Key cannot be null or empty");
        }
        if (value == null)
        {
            throw new ArgumentNullException("Value cannot be null");
        }
        if (!_samples.ContainsKey(key))
        {
            throw new ArgumentException("Key does not exist");
        }
        _samples[key] = value;
    }

    public void Delete(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException("Key cannot be null or empty");
        }
        if (!_samples.ContainsKey(key))
        {
            throw new ArgumentException("Key does not exist");
        }
        _samples.Remove(key);
    }

    public object Get(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException("Key cannot be null or empty");
        }
        if (!_samples.ContainsKey(key))
        {
            throw new ArgumentException("Key does not exist");
        }
        return _samples[key];
    }
}
