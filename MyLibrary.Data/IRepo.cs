namespace MyLibrary.Data;

public interface IRepo
{
    void Add(string key, object value);
    void Delete(string key);
    object Get(string key);
    void Update(string key, object value);
    Dictionary<string, object> GetSamples();
}
