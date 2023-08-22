
namespace EventModuleApi.Infrastructure.Helper;

public static class PaginationHelper
{
    public static PagedResponse<T> CreatePagedReponse<T>(T pagedData, Request.PaginatedReq paginatedReq, int totalRecords)
    {
        var response = new PagedResponse<T>(pagedData, paginatedReq.PageNumber, paginatedReq.PageSize);
        var totalPages = (double)totalRecords / paginatedReq.PageSize;
        int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
        response.Data = pagedData;
        response.TotalPages = roundedTotalPages;
        response.TotalRecords = totalRecords;

        return response;
    }
}