namespace Howest.MagicCards.Shared.Filters;

public class PaginationFilter
{
    private int _maxPageSize = 20;
    private int _pageSize = 500;
    private int _pageNumber = 1;

    public int MaxPageSize
    {
        get { return _maxPageSize; }
        set { _maxPageSize = (value < 1) ? 10 : value; }
    }

    public int PageNumber
    {
        get { return _pageNumber; }
        set { _pageNumber = (value < 1) ? 1 : value; }
    }

    public int PageSize
    {
        get { return _pageSize > MaxPageSize ? MaxPageSize : _pageSize; }
        set { _pageSize = (value > MaxPageSize || value < 1) ? MaxPageSize : value; }
    }
}