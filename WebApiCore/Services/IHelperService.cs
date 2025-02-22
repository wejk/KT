namespace WebApiCore.Services;

public interface IHelperService
{
    string[] Get(int count, Categories key);
    Dictionary<string, IEnumerable<string>> GetSamples();
}
