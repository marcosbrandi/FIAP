#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TechChallengeFIAP.API/TechChallengeFIAP.API.csproj", "TechChallengeFIAP.API/"]
COPY ["TechChallengeFIAP.Core/TechChallengeFIAP.Core.csproj", "TechChallengeFIAP.Core/"]
COPY ["TechChallengeFIAP.Infrastructure/TechChallengeFIAP.Infrastructure.csproj", "TechChallengeFIAP.Infrastructure/"]
RUN dotnet restore "./TechChallengeFIAP.API/TechChallengeFIAP.API.csproj"
COPY . .
WORKDIR "/src/TechChallengeFIAP.API"
RUN dotnet build "./TechChallengeFIAP.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TechChallengeFIAP.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechChallengeFIAP.API.dll"]