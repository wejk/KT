namespace WebApiCore.Services;

public interface IHelperService
{
    string[] Get(int count);
    Dictionary<string, object> GetSamples();
}
