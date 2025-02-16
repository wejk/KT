namespace WebApiCore.Services;

public interface IHelperService
{
    string[] Get(int count);
    Dictionary<string, IList<string>> GetSamples();
}
