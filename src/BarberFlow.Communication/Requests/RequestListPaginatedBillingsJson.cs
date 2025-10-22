using System;

namespace BarberFlow.Communication.Requests;

public class RequestListPaginatedBillingsJson
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
    public string? SearchTerm { get; set; }
    public string? OrderBy { get; set; }
    public bool? IsDescending { get; set; }

}
