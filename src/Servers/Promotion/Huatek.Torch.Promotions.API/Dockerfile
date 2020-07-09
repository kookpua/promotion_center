#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Servers/Promotion/Huatek.Torch.Promotions.API/Huatek.Torch.Promotions.API.csproj", "Servers/Promotion/Huatek.Torch.Promotions.API/"]
COPY ["Servers/Promotion/Huatek.Torch.Promotions.Infrastructure/Huatek.Torch.Promotions.Infrastructure.csproj", "Servers/Promotion/Huatek.Torch.Promotions.Infrastructure/"]
COPY ["Shared/Huatek.Torch.Infrastructure.Core/Huatek.Torch.Infrastructure.Core.csproj", "Shared/Huatek.Torch.Infrastructure.Core/"]
COPY ["Shared/Huatek.Torch.Domain.Abstractions/Huatek.Torch.Domain.Abstractions.csproj", "Shared/Huatek.Torch.Domain.Abstractions/"]
COPY ["Servers/Promotion/Huatek.Torch.Promotions.Domin/Huatek.Torch.Promotions.Domain.csproj", "Servers/Promotion/Huatek.Torch.Promotions.Domin/"]
COPY ["Servers/Promotion/Huatek.Torch.Promotions.Service/Huatek.Torch.Promotions.Service.csproj", "Servers/Promotion/Huatek.Torch.Promotions.Service/"]
RUN dotnet restore "Servers/Promotion/Huatek.Torch.Promotions.API/Huatek.Torch.Promotions.API.csproj"
COPY . .
WORKDIR "/src/Servers/Promotion/Huatek.Torch.Promotions.API"
RUN dotnet build "Huatek.Torch.Promotions.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Huatek.Torch.Promotions.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Huatek.Torch.Promotions.API.dll"]