FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/WebScraper/WebScraper.API/WebScraper.API.csproj", "Services/WebScraper/WebScraper.API/"]
COPY ["Services/WebScraper/WebScraper.Application/WebScraper.Application.csproj", "Services/WebScraper/WebScraper.Application/"]
COPY ["Services/WebScraper/WebScraper.Core/WebScraper.Core.csproj", "Services/WebScraper/WebScraper.Core/"]
COPY ["Services/WebScraper/WebScraper.Infrastructure/WebScraper.Infrastructure.csproj", "Services/WebScraper/Basket.Infrastructure/"]

RUN dotnet restore "Services/WebScraper/WebScraper.API/WebScraper.API.csproj"
COPY . .
WORKDIR "/src/Services/WebScraper/WebScraper.API"
RUN dotnet build "WebScraper.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebScraper.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebScraper.API.dll"]
