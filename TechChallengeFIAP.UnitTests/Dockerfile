#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TechChallengeFIAP.UnitTests/TechChallengeFIAP.UnitTests.csproj", "TechChallengeFIAP.UnitTests/"]
COPY ["Fiap.Core/Fiap.Core.csproj", "Fiap.Core/"]
COPY ["Fiap.MessageBus/Fiap.MessageBus.csproj", "Fiap.MessageBus/"]
COPY ["Fiap.WebAPI.Core/Fiap.WebAPI.Core.csproj", "Fiap.WebAPI.Core/"]
COPY ["TechChallenge.API/TechChallenge.API.csproj", "TechChallenge.API/"]
COPY ["TechChallenge.Domain/TechChallenge.Domain.csproj", "TechChallenge.Domain/"]
COPY ["TechChallenge.Infrastructure/TechChallenge.Infrastructure.csproj", "TechChallenge.Infrastructure/"]
COPY ["TechChallenge.Producer/TechChallenge.Producer.csproj", "TechChallenge.Producer/"]
RUN dotnet restore "./TechChallengeFIAP.UnitTests/TechChallengeFIAP.UnitTests.csproj"
COPY . .
WORKDIR "/src/TechChallengeFIAP.UnitTests"
RUN dotnet build "./TechChallengeFIAP.UnitTests.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TechChallengeFIAP.UnitTests.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechChallengeFIAP.UnitTests.dll"]