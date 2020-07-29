namespace Station.SortApply.Helper
{
    public interface IPropertyChckerService
    {
        bool TypeHasProperties<TSource>(string fields);
    }
}