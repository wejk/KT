using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MyLibrary.Data;

public sealed class JsonRepo : IRepo
{
    private readonly ILogger<JsonRepo> _logger;
    private readonly string _filePath;

    public JsonRepo(ILogger<JsonRepo> logger, string fileName = "Data.json")
    {
        _logger = logger;
        _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
    }

    private Model Read()
    {
        lock (this)
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    throw new FileNotFoundException($"File not found: {_filePath}");
                }

                var json = File.ReadAllText(_filePath);
                if (string.IsNullOrEmpty(json))
                {
                    return new Model();
                }
                var model = JsonSerializer.Deserialize<Model>(json);
                return model!;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error deserializing data.json");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading data.json");
                throw;
            }
        }
    }

    public Model GetAllData()
    {
        return Read();
    }

    public void Add(string key, IList<string> value)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException(nameof(key), "Key cannot be null or empty");
        }
        if (value?.Count == 0)
        {
            throw new ArgumentNullException(nameof(value), "Value cannot be null");
        }

        var model = Read();
        if (model.Samples.ContainsKey(key))
        {
            throw new ArgumentException("Key already exists", nameof(key));
        }
        model.Samples.Add(key, value.ToArray());

        WriteToJsonFile(model);
    }

    private void WriteToJsonFile(Model model)
    {
        lock (this)
        {
            try
            {
                var json = JsonSerializer.Serialize(model, new JsonSerializerOptions { WriteIndented = false });
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while writing to Data.json");
                throw;
            }
        }
    }

    public void Update(string key, IList<string> value)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException(nameof(key), "Key cannot be null or empty");
        }
        if (value.Count == 0)
        {
            throw new ArgumentNullException(nameof(value), "Value cannot be null");
        }

        var model = Read();
        if (!model.Samples.ContainsKey(key))
        {
            throw new ArgumentException("Key does not exist", nameof(key));
        }
        model.Samples[key] = value.ToArray();
        WriteToJsonFile(model);
    }

    public void Delete(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException(nameof(key), "Key cannot be null or empty");
        }

        var model = Read();
        if (!model.Samples.ContainsKey(key))
        {
            throw new ArgumentException("Key does not exist", nameof(key));
        }
        model.Samples.Remove(key);
        WriteToJsonFile(model);
    }

    public IList<string> Get(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException(nameof(key), "Key cannot be null or empty");
        }

        var samples = Read().Samples;
        if (!samples.TryGetValue(key, out var value))
        {
            throw new ArgumentException("Key does not exist", nameof(key));
        }
        return value;
    }

    public Dictionary<string, IList<string>> GetSamples()
    {
        return Read().Samples;
    }
}
