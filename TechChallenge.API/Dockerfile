# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TechChallenge.API/TechChallenge.API.csproj", "TechChallenge.API/"]
COPY ["Fiap.Core/Fiap.Core.csproj", "Fiap.Core/"]
COPY ["Fiap.MessageBus/Fiap.MessageBus.csproj", "Fiap.MessageBus/"]
COPY ["Fiap.WebAPI.Core/Fiap.WebAPI.Core.csproj", "Fiap.WebAPI.Core/"]
COPY ["TechChallenge.Domain/TechChallenge.Domain.csproj", "TechChallenge.Domain/"]
COPY ["TechChallenge.Infrastructure/TechChallenge.Infrastructure.csproj", "TechChallenge.Infrastructure/"]
RUN dotnet restore "./TechChallenge.API/TechChallenge.API.csproj"
COPY . .
WORKDIR "/src/TechChallenge.API"
RUN dotnet build "./TechChallenge.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TechChallenge.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechChallenge.API.dll"]