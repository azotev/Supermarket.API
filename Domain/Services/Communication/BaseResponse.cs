namespace Supermarket.API.Domain.Services.Communication
{
    public abstract class BaseResponse
    {
        public bool Success { get; init; }
        public string Message { get; init; }

        public BaseResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}