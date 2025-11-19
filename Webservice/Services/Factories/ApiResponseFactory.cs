using Contracts.Services;

namespace Webservice.Services.Factories
{
    public static class ApiResponseFactory<TData> where TData : class
    {
        public static ApiResponse<TData> CreateResponse(bool success, int statusCode, TData? data = null, string? message = null)
        {
            return new ApiResponse<TData>()
            {
                Success = success,
                StatusCode = statusCode,
                Data = data,
                Message = message
            };

        }
    }
}
