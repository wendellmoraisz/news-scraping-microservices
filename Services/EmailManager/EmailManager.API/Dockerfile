FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/EmailManager/EmailManager.API/EmailManager.API.csproj", "Services/EmailManager/EmailManager.API/"]
COPY ["Services/EmailManager/EmailManager.Application/EmailManager.Application.csproj", "Services/EmailManager/EmailManager.Application/"]
COPY ["Services/EmailManager/EmailManager.Core/EmailManager.Core.csproj", "Services/EmailManager/EmailManager.Core/"]
COPY ["Services/EmailManager/EmailManager.Infrastructure/EmailManager.Infrastructure.csproj", "Services/EmailManager/EmailManager.Infrastructure/"]

RUN dotnet restore "Services/EmailManager/EmailManager.API/EmailManager.API.csproj"
COPY . .
WORKDIR "/src/Services/EmailManager/EmailManager.API"
RUN dotnet build "EmailManager.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmailManager.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmailManager.API.dll"]
