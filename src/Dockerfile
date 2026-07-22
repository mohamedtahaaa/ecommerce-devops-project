# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
# Copy solution file
COPY ECommerce.API.sln .
# Copy project files
COPY ECommerce.API/ECommerce.API.csproj ECommerce.API/
COPY ECommerce.BLL/ECommerce.BLL.csproj ECommerce.BLL/
COPY ECommerce.DAL/ECommerce.DAL.csproj ECommerce.DAL/
COPY ECommerce.Common/ECommerce.Common.csproj ECommerce.Common/
# Restore dependencies
RUN dotnet restore
COPY . .
RUN dotnet publish ECommerce.API/ECommerce.API.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet","ECommerce.API.dll"]