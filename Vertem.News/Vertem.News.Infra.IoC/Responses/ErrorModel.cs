namespace Vertem.News.Infra.Responses
{
    public class ErrorModel
    {
        public string Type { get; private set; }
        public string Detail { get; private set; }

        public ErrorModel(string type, string detail)
        {
            Type = type;
            Detail = detail;
        }
    }
}
