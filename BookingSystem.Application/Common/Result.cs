namespace BookingSystem.Application.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public T? Data { get; private set; }
        public string? ErrorEn { get; private set; }
        public string? ErrorAr { get; private set; }

        private Result(bool isSuccess, T? data, string? errorEn, string? errorAr)
        {
            IsSuccess = isSuccess;
            Data = data;
            ErrorEn = errorEn;
            ErrorAr = errorAr;
        }

        public static Result<T> Success(T data) =>
            new(true, data, null, null);

        public static Result<T> Failure(string errorEn, string errorAr) =>
            new(false, default, errorEn, errorAr);
    }
}