namespace Howest.MagicCards.Shared.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        // TotalPages could be used to set upper limit in PageSize validation.
        public int TotalPages { get => (int)Math.Ceiling(TotalRecords / (double)PageSize); }
        public int TotalRecords { get; set; }

        public PagedResponse(T? data, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Data = data;
        }
    }
}
