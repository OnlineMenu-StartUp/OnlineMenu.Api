namespace OnlineMenu.Api.ExceptionHandling
{
    public class ErrorDto
    {
        public ErrorDto(string message, object data = null)
        {
            Message = message;
            Data = data;
        }

        public string Message { get; }

        public object Data { get; }
    }
}