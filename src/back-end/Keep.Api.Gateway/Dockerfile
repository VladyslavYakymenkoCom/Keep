#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Keep.Api.Gateway/Keep.Api.Gateway.csproj", "Keep.Api.Gateway/"]
RUN dotnet restore "Keep.Api.Gateway/Keep.Api.Gateway.csproj"
COPY . .
WORKDIR "/src/Keep.Api.Gateway"
RUN dotnet build "Keep.Api.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Keep.Api.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Keep.Api.Gateway.dll"]