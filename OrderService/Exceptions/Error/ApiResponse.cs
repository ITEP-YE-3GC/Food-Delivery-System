namespace OrderService.Exceptions.Error
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaulForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaulForStatusCode(int statusCode)
        {
            var result = statusCode switch
            {
                400 => "The request cannot be processed due to bad syntax. Please check your input.",
                401 => "Access denied. Please provide valid credentials to proceed.",
                404 => "The requested resource could not be found. Please verify the URL.",
                500 => "An unexpected error occurred on the server. We are working to resolve the issue.",
                _ => "An unknown error occurred. Please try again later."
            };
            return result;
        }
    }
}