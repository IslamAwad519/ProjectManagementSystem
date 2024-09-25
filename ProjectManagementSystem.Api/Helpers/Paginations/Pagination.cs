using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Api.DTOs.Auth;

namespace ProjectManagementSystem.Api.Helpers.Paginations
{
   
    public class Pagination<T>
    {
        public bool IsSuccess { get; set; }
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public List<T> Data { get; set; }
        public string Message { get; set; }

        public Pagination( bool isSuccess,List<T> data, int totalCount, int pageIndex, int pageSize ,string message)
        {
            IsSuccess = isSuccess;
            TotalCount = totalCount;
            PageIndex = pageIndex;
            PageSize = pageSize;
            Data = data;
            Message = message;
        }
    }



}
