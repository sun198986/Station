namespace Station.Repository.RepositoryPattern.SortApply
{
    public interface IPropertyChckerService
    {
        bool TypeHasProperties<TSource>(string fields);
    }
}