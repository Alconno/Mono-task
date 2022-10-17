namespace Vehicles
{
    public interface IPagination
    {
        bool HasNextPage { get; }
        bool HasPreviousPage { get; }
        int pageIndex { get; }
        int totalPages { get; }
    }
}