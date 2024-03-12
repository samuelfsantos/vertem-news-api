FROM mcr.microsoft.com/dotnet/aspnet:8.0.1 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0.101 AS build
WORKDIR /src
COPY ["./Vertem.News.Api/Vertem.News.Api.csproj", "Vertem.News.Api/"]
RUN dotnet restore "Vertem.News.Api/Vertem.News.Api.csproj"
COPY . .

WORKDIR "Vertem.News.Api"
RUN dotnet build "Vertem.News.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Vertem.News.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ./Vertem.News.Api/vertem_news.db .

ENTRYPOINT ["dotnet", "Vertem.News.Api.dll"]
