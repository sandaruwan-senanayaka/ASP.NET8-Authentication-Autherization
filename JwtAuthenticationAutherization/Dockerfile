# Use the official .NET 8 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the project files and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application and build the release
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Use the official .NET runtime image for the final build
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/publish .

EXPOSE 5008
EXPOSE 7117

ENTRYPOINT ["dotnet", "JwtAuthenticationAutherization.dll"]
