FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/EmailSender/EmailSender.API/EmailSender.API.csproj", "Services/EmailSender/EmailSender.API/"]
COPY ["Services/EmailSender/EmailSender.Application/EmailSender.Application.csproj", "Services/EmailSender/EmailSender.Application/"]
COPY ["Services/EmailSender/EmailSender.Core/EmailSender.Core.csproj", "Services/EmailSender/EmailSender.Core/"]
COPY ["Services/EmailSender/EmailSender.Infrastructure/EmailSender.Infrastructure.csproj", "Services/EmailSender/EmailSender.Infrastructure/"]

RUN dotnet restore "Services/EmailSender/EmailSender.API/EmailSender.API.csproj"
COPY . .
WORKDIR "/src/Services/EmailSender/EmailSender.API"
RUN dotnet build "EmailSender.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmailSender.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmailSender.API.dll"]
