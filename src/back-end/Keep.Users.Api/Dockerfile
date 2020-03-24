#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Keep.Users.Api/Keep.Users.Api.csproj", "Keep.Users.Api/"]
RUN dotnet restore "Keep.Users.Api/Keep.Users.Api.csproj"
COPY . .
WORKDIR "/src/Keep.Users.Api"
RUN dotnet build "Keep.Users.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Keep.Users.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Keep.Users.Api.dll"]