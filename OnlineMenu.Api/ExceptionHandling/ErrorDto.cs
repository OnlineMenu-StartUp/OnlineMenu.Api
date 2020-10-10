namespace OnlineMenu.Api.ExceptionHandling
{
    public class ErrorDto
    {
        public ErrorDto(string message, object data = null)
        {
            Message = message;
            Data = data;
        }

        public string Message { get; private set; }

        public object Data { get; private set; }
    }
}