using Vertem.News.Services.NewsApiOrg.Constants;

namespace Vertem.News.Services.NewsApiOrg.Models
{
    public class Error
    {
        public ErrorCodes Code { get; set; }
        public string Message { get; set; }
    }
}
