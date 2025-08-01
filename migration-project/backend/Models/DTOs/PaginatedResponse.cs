using X.PagedList;

namespace Backend.Models.DTOs;

public class PaginatedResponse<T> where T : class
{
    public IPagedList<T> Items { get; set; }
    public int Page { get; set; }
    public int TotalPage { get; set; }
}