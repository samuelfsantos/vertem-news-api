namespace Vertem.News.Infra.Responses
{
    public class ErrorResponse
    {
        public string Instance { get; private set; }
        public IEnumerable<ErrorModel> Errors { get; private set; }

        public ErrorResponse(string instance, IEnumerable<ErrorModel> errors)
        {
            Instance = instance;
            Errors = errors;
        }
    }
}
