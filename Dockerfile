# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
# Expose port 44366 as the service should run on this port in Docker
EXPOSE 44366


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TA9_WebSocket_Receiver/TA9_WebSocket_MessageSender.csproj", "TA9_WebSocket_Receiver/"]
RUN dotnet restore "./TA9_WebSocket_Receiver/TA9_WebSocket_MessageSender.csproj"
COPY . .
WORKDIR "/src/TA9_WebSocket_Receiver"
RUN dotnet build "./TA9_WebSocket_MessageSender.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TA9_WebSocket_MessageSender.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Set the listening port of the application to 44366
ENV ASPNETCORE_URLS=http://+:44366;https://+:44367

ENTRYPOINT ["dotnet", "TA9_WebSocket_MessageSender.dll"]