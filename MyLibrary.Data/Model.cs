namespace MyLibrary.Data
{
    public sealed class Model
    {
        public Dictionary<string, IList<string>> Samples { get; set; } = new Dictionary<string, IList<string>>();
    }
}
