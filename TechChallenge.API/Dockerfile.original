#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Fiap.Clientes.API/Fiap.Clientes.API.csproj", "src/services/Fiap.Clientes.API/"]
RUN dotnet restore "src/services/Fiap.Clientes.API/Fiap.Clientes.API.csproj"
COPY . .
WORKDIR "/src/src/Fiap.Clientes.API"
RUN dotnet build "Fiap.Clientes.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fiap.Clientes.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fiap.Clientes.API.dll"]
