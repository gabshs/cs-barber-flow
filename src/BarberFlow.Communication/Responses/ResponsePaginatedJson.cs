namespace BarberFlow.Communication.Responses;

public class ResponsePaginatedJson<T>
{
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? OrderBy { get; set; }
    public bool? IsDescending { get; set; }
    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
}
