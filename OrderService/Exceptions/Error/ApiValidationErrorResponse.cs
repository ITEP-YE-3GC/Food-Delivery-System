namespace OrderService.Exceptions.Error
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public ApiValidationErrorResponse()
            : base(400) { }

        public IEnumerable<string> Errors { set; get; }
    }
}