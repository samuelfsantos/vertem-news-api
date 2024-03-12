using FluentValidation;
using System.Text.Json.Serialization;
using Vertem.News.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDatabaseConfig(builder.Configuration);
builder.Services.AddDependencyInjectionConfig(builder.Configuration);
builder.Services.AddMediatrConfig();
builder.Services.AddValidatorsFromAssembly(typeof(Vertem.News.Domain.AssemblyReference).Assembly);
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig();
builder.Services.AddNewsApiOrgServiceConfig(builder.Configuration);
builder.Services.AddCorsConfig();

var app = builder.Build();
app.UseSwaggerConfig();
app.UseCorsConfig();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();