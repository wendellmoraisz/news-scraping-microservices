FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["EmailSender/EmailSender.API/EmailSender.API.csproj", "EmailSender/EmailSender.API/"]
RUN dotnet restore "EmailSender/EmailSender.API/EmailSender.API.csproj"
COPY . .
WORKDIR "/src/EmailSender/EmailSender.API"
RUN dotnet build "EmailSender.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmailSender.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmailSender.API.dll"]
