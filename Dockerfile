# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["WebApiCiCd/WebApiCiCd.csproj", "WebApiCiCd/"]
RUN dotnet restore "WebApiCiCd/WebApiCiCd.csproj"
COPY . .
WORKDIR "/src/WebApiCiCd"
RUN dotnet build "WebApiCiCd.csproj" -c Release -o /app/build

# Test stage
FROM build AS test
WORKDIR /src
RUN dotnet test --logger:trx

# Publish stage
FROM build AS publish
RUN dotnet publish "WebApiCiCd.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApiCiCd.dll"]