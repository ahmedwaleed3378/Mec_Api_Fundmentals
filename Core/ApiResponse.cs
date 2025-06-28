// Models/ApiResponse.cs
namespace Mec_Api_Fundmentals.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public object Errors { get; set; }

        private ApiResponse(T data, string message, bool success, object errors = null)
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = errors;
        }

        public static ApiResponse<T> CreateSuccess(T data, string message = "Operation completed successfully")
        {
            return new ApiResponse<T>(data, message, true);
        }

        public static ApiResponse<T> CreateError(string message, object errors = null, T data = default)
        {
            return new ApiResponse<T>(data, message, false, errors);
        }
    }
}
