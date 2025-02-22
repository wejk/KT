namespace MyLibrary.Data
{
    public sealed class Model
    {
        public Dictionary<string, IEnumerable<string>> Samples { get; set; } = new Dictionary<string, IEnumerable<string>>();
    }
}
