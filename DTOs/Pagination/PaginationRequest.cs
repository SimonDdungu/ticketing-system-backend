namespace Ticketing_backend.DTOs.Pagination;

public class PaginationRequest
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public string? SortBy { get; set; }

    public string SortOrder { get; set; } = "asc";
}