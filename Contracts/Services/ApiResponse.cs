namespace Contracts.Services
{
    public class ApiResponse<TData> where TData : class
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public TData? Data { get; set; }
    }
}
