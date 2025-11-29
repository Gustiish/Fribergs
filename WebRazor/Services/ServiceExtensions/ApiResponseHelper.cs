using Contracts.Services;

namespace WebRazor.Services.ServiceExtensions
{
    public static class ApiResponseHelper
    {
        public static Data? TryGetData<Data>(this ApiResponse<Data> response) where Data : class
        {
            if (response.Success == false)
                return null;

            return response.Data;
        }

    }
}
