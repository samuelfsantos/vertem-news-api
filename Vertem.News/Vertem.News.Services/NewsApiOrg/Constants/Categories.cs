using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Vertem.News.Services.NewsApiOrg.Constants
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Categories
    {
        Business,
        Entertainment,
        Health,
        Science,
        Sports,
        Technology
    }
}
