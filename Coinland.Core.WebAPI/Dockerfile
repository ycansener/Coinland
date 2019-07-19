FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Coinland.Core.WebAPI/Coinland.Core.WebAPI.csproj", "Coinland.Core.WebAPI/"]
COPY ["Coinland.Core.Infrastructure/Coinland.Core.Infrastructure.csproj", "Coinland.Core.Infrastructure/"]
COPY ["Coinland.Core.Domain/Coinland.Core.Domain.csproj", "Coinland.Core.Domain/"]
RUN dotnet restore "Coinland.Core.WebAPI/Coinland.Core.WebAPI.csproj"
COPY . .
WORKDIR "/src/Coinland.Core.WebAPI"
RUN dotnet build "Coinland.Core.WebAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Coinland.Core.WebAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Coinland.Core.WebAPI.dll"]