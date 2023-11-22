FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["EmailManager/EmailManager.API/EmailManager.API.csproj", "EmailManager/EmailManager.API/"]
RUN dotnet restore "EmailManager/EmailManager.API/EmailManager.API.csproj"
COPY . .
WORKDIR "/src/EmailManager/EmailManager.API"
RUN dotnet build "EmailManager.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmailManager.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmailManager.API.dll"]
