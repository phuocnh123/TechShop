namespace Infrastructure.Commons
{
    public class ResponseResult
    {
        public ResponseResult(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;

        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
