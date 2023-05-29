using MediatR;
using Newtonsoft.Json;
using RestSharp;
using System.Diagnostics;
using System.Net;
using Vertem.News.Domain.Entities;
using Vertem.News.Domain.Interfaces;
using Vertem.News.Services.Configurations;
using Vertem.News.Services.NewsApiOrg.Constants;
using Vertem.News.Services.NewsApiOrg.Models;

namespace Vertem.News.Services.NewsApiOrg
{
    public class NewsApiOrgService : INewsApiOrgService
    {
        private readonly NewsApiOrgServiceConfiguration _configuration;
        public NewsApiOrgService(NewsApiOrgServiceConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Noticia>> ObterNoticias(string request)
        {
            Categories categoria;
            Enum.TryParse(request, out categoria);

            var topHeadlinesRequest = new TopHeadlinesRequest()
            {
                Category = categoria
            };

            var artigosResult = await ObterArtigos(topHeadlinesRequest);
            if (artigosResult.Status == Statuses.Ok)
            {
                var artigos = artigosResult.Articles ?? new List<Article>();
                var noticias = artigos
                                .Where(a =>
                                    !String.IsNullOrWhiteSpace(a.Title) &&
                                    !String.IsNullOrWhiteSpace(a.Description) &&
                                    !String.IsNullOrWhiteSpace(a.Content) &&
                                    !String.IsNullOrWhiteSpace(a.Title) &&
                                    !String.IsNullOrWhiteSpace(a?.Source?.Name) &&
                                    a.PublishedAt.HasValue)
                                .Select(a => new Noticia(
                                    a.Title,
                                    a.Description,
                                    a.Content,
                                    topHeadlinesRequest.Category.Value.ToString(),
                                    a.Source.Name,
                                    a.PublishedAt.Value,
                                    a.UrlToImage,
                                    a.Author)).ToList();

                return noticias;
            }
            else
                throw new Exception(artigosResult?.Error?.Message);
        }

        private async Task<ArticlesResult> ObterArtigos(TopHeadlinesRequest topHeadlinesRequest)
        {
            var articlesResult = new ArticlesResult();
            var resquest = new RestRequest();

            var client = new RestClient
            {
                BaseUrl = new Uri($"{_configuration.Url}/v2/top-headlinessss")
            };

            resquest.AddQueryParameter("apiKey", _configuration.ChaveSistema);
            resquest.AddQueryParameter("category", topHeadlinesRequest.Category.Value.ToString().ToLowerInvariant());
            resquest.AddQueryParameter("PageSize ", "100");
            resquest.AddQueryParameter("country", "us");
            resquest.Method = Method.GET;

            var response = await client.ExecuteAsync(resquest);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response.Content);

                articlesResult.Status = apiResponse.Status;
                if (articlesResult.Status == Statuses.Ok)
                {
                    articlesResult.TotalResults = apiResponse.TotalResults;
                    articlesResult.Articles = apiResponse.Articles;
                }
                else
                {
                    ErrorCodes errorCode = ErrorCodes.UnknownError;
                    try
                    {
                        errorCode = (ErrorCodes)apiResponse.Code;
                    }
                    catch (Exception)
                    {
                        Debug.WriteLine("A API retornou um código de erro que não era esperado: " + apiResponse.Code);
                    }

                    articlesResult.Error = new Error
                    {
                        Code = errorCode,
                        Message = apiResponse.Message
                    };
                }
            }
            else
            {
                articlesResult.Status = Statuses.Error;
                articlesResult.Error = new Error
                {
                    Message = "Não foi possível realizar a integração com a API solicitada."
                };
            }

            return articlesResult;
        }
    }
}
