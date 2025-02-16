namespace MyLibrary.Data;

public interface IRepo
{
    void Add(string key, IList<string> value);
    void Delete(string key);
    IList<string> Get(string key);
    void Update(string key, IList<string> value);
    Dictionary<string, IList<string>> GetSamples();
}
