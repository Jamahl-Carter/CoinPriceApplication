#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CoinPrice.Api/CoinPrice.Api.csproj", "CoinPrice.Api/"]
RUN dotnet restore "CoinPrice.Api/CoinPrice.Api.csproj"
COPY . .
WORKDIR "/src/CoinPrice.Api"
RUN dotnet build "CoinPrice.Api.csproj" -c Release -o /app/build

FROM build AS test
WORKDIR "/src/CoinPrice.Business.Test"
RUN dotnet test -c Release -o /test
WORKDIR "/src/CoinPrice.Data.Test"
RUN dotnet test -c Release -o /test

FROM build AS publish
RUN dotnet publish "CoinPrice.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoinPrice.Api.dll"]