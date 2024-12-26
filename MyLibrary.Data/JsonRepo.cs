using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MyLibrary.Data;
public class JsonRepo : IRepo
{
    public Dictionary<string, object> Samples { get; set; } = new Dictionary<string, object>();

    private readonly ILogger<JsonRepo> _logger;
    public JsonRepo(ILogger<JsonRepo> logger)
    {
        _logger = logger;
    }
    public void Read()
    {
        try
        {
            using (StreamReader r = new StreamReader("data.json"))
            {
                string json = r.ReadToEnd();
                var samples = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
                if (samples != null)
                {
                    Samples = samples;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading data.json");
        }
    }
    public Dictionary<string, object> GetSamples()
    {
        return Samples;
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
        if (Samples.ContainsKey(key))
        {
            throw new ArgumentException("Key already exists");
        }
        Samples.Add(key, value);
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
        if (!Samples.ContainsKey(key))
        {
            throw new ArgumentException("Key does not exist");
        }
        Samples[key] = value;
    }

    public void Delete(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException("Key cannot be null or empty");
        }
        if (!Samples.ContainsKey(key))
        {
            throw new ArgumentException("Key does not exist");
        }
        Samples.Remove(key);
    }

    public object Get(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException("Key cannot be null or empty");
        }
        if (!Samples.ContainsKey(key))
        {
            throw new ArgumentException("Key does not exist");
        }
        return Samples[key];
    }
}
