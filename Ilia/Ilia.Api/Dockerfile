#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Ilia.Api/Ilia.Api.csproj", "Ilia.Api/"]
COPY ["Ilia.CrossCutting/Ilia.CrossCutting.csproj", "Ilia.CrossCutting/"]
COPY ["Ilia.Domain/Ilia.Domain.csproj", "Ilia.Domain/"]
COPY ["Ilia.Business/Ilia.Business.csproj", "Ilia.Business/"]
COPY ["Ilia.Data/Ilia.Data.csproj", "Ilia.Data/"]
RUN dotnet restore "Ilia.Api/Ilia.Api.csproj"
COPY . .
WORKDIR "/src/Ilia.Api"
RUN dotnet build "Ilia.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ilia.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ilia.Api.dll"]