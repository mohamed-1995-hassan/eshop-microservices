# Simple Dockerfile that uses pre-published output
# Build locally first: dotnet publish -c Release -o ./publish
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

COPY WebApps/Shopping.Web/publish/ .
ENTRYPOINT ["dotnet", "Shopping.Web.dll"]
