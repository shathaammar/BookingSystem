namespace BookingSystem.Application.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string MessageEn { get; set; } = string.Empty;
        public string MessageAr { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static ApiResponse<T> Ok(T data, string messageEn, string messageAr) => new()
        {
            Success = true,
            Data = data,
            MessageEn = messageEn,
            MessageAr = messageAr,
        };

        public static ApiResponse<T> Fail(string messageEn, string messageAr) => new()
        {
            Success = false,
            Data = default,
            MessageEn = messageEn,
            MessageAr = messageAr
        };
    }
}