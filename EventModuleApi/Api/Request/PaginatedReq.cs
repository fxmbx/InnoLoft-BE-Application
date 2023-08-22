namespace EventModuleApi.Request;
public class PaginatedReq
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public PaginatedReq()
    {
        PageNumber = 1;
        PageSize = 10;
    }
    public PaginatedReq(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize > 10 || pageSize < 1 ? 10 : pageSize;
    }
}
