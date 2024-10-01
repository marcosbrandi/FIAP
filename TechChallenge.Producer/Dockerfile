#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8082:8082
EXPOSE 8083:8083

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY ["TechChallenge.Producer/TechChallenge.Producer.csproj", "TechChallenge.Producer/"]
COPY ["Fiap.Core/Fiap.Core.csproj", "Fiap.Core/"]
COPY ["Fiap.MessageBus/Fiap.MessageBus.csproj", "Fiap.MessageBus/"]
COPY ["Fiap.WebAPI.Core/Fiap.WebAPI.Core.csproj", "Fiap.WebAPI.Core/"]
RUN dotnet restore "./TechChallenge.Producer/TechChallenge.Producer.csproj"
COPY . .
WORKDIR "/src/TechChallenge.Producer"
RUN dotnet build "./TechChallenge.Producer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "./TechChallenge.Producer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechChallenge.Producer.dll"]