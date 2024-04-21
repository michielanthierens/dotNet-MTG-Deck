﻿namespace Howest.MagicCards.Shared.Filters;

public class PaginationFilter
{
    const int _maxPageSize = 10;

    private int _pageSize = 50;
    private int _pageNumber = 1;

    public int MaxPageSize { get; set; } = _maxPageSize;


    public int PageNumber
    {
        get { return _pageNumber; }
        set { _pageNumber = (value < 1) ? 1 : value; }
    }

    public int PageSize
    {
        get { return _pageSize > MaxPageSize ? MaxPageSize : _pageSize; }
        set { _pageSize = (value < 1) ? MaxPageSize : value; }
    }
}