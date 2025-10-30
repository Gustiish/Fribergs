namespace ApplicationCore.Interfaces
{
    public interface ICarReference
    {
        IReadOnlyDictionary<string, List<string>> GetBrandModels();
    }
}
