using Vertem.News.Infra.Base;
using System.Net;

namespace Vertem.News.Infra.Responses
{
    public class RequestResult<TResponse> where TResponse : BaseOutput
    {
        public HttpStatusCode StatusCode { get; private set; }
        public TResponse? Data { get; private set; }
        public IEnumerable<TResponse>? MultipleData { get; private set; }
        public IEnumerable<ErrorModel> Errors { get; private set; }

        public RequestResult(HttpStatusCode statusCode, TResponse? data, IEnumerable<ErrorModel> errors)
        {
            StatusCode = statusCode;
            Data = data;
            Errors = errors;
            MultipleData = null;
        }

        public RequestResult(HttpStatusCode statusCode, IEnumerable<TResponse>? multipleData, IEnumerable<ErrorModel> errors)
        {
            StatusCode = statusCode;
            Data = null;
            Errors = errors;
            MultipleData = multipleData;
        }

        public RequestResult(HttpStatusCode statusCode, IEnumerable<ErrorModel> errors)
        {
            StatusCode = statusCode;
            Data = null;
            Errors = errors;
            MultipleData = null;
        }
    }
}
