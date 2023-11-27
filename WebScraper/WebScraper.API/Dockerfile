FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebScraper/WebScraper.Application/WebScraper.Application.csproj", "WebScraper/WebScraper.Application/"]
RUN dotnet restore "WebScraper/WebScraper.Application/WebScraper.Application.csproj"
COPY . .
WORKDIR "/src/WebScraper/WebScraper.Application"
RUN dotnet build "WebScraper.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebScraper.Application.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebScraper.Application.dll"]
