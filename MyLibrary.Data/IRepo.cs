namespace MyLibrary.Data;

public interface IRepo
{
    void Add(string key, IEnumerable<string> value);
    void Delete(string key);
    IEnumerable<string> Get(string key, int count);
    void Update(string key, IEnumerable<string> value);
    Dictionary<string, IEnumerable<string>> GetSamples();
}
