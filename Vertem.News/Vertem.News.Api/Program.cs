using Confluent.Kafka;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vertem.News.Domain.Interfaces;
using Vertem.News.Infra.Data;
using Vertem.News.Infra.Data.Contexts;
using Vertem.News.Infra.Data.Repositories;
using Vertem.News.Infra.PipelineBehavior;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Vertem.News.Api.Configurations;
using Vertem.News.Services.NewsApiOrg;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlite(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddScoped<INoticiaRepository, NoticiaRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<INewsApiOrgService, NewsApiOrgService>();

builder.Services.AddMediatR(typeof(Vertem.News.Application.AssemblyReference).Assembly);
builder.Services.AddMediatR(typeof(Vertem.News.Domain.AssemblyReference).Assembly);

builder.Services.Scan(scan => scan.FromApplicationDependencies()
    .AddClasses(@class => @class.AssignableTo(typeof(IShallowValidator<>))).AsImplementedInterfaces())
    .AddTransient(typeof(IPipelineBehavior<,>), typeof(ShallowValidationBehavior<,>));

builder.Services.Scan(scan => scan.FromApplicationDependencies()
    .AddClasses(@class => @class.AssignableTo(typeof(IDeepValidator<>))).AsImplementedInterfaces())
    .AddTransient(typeof(IPipelineBehavior<,>), typeof(DeepValidationBehavior<,>));

builder.Services.AddValidatorsFromAssembly(typeof(Vertem.News.Domain.AssemblyReference).Assembly);

builder.Services.AddSingleton(new ProducerBuilder<string, string>(new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("bootstrap.servers", builder.Configuration.GetConnectionString("Kafka"))
            }));

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Vertem - Desafio técnico",
        Version = "v1",
        Description = "API REST notícias.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Samuel Ferreira Santos",
            Email = "samuel.santos.si@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/samuel-santos-0a243856/")
        }
    });
    c.UseInlineDefinitionsForEnums();
});

builder.Services.AddNewsApiOrgServiceConfiguration(builder.Configuration);

//services cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app cors
app.UseCors("corsapp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
