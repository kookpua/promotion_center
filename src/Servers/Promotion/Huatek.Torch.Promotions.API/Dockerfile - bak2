FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
WORKDIR /app
EXPOSE 8001
COPY . .
ENV ASPNETCORE_URLS http://+:8001
ENTRYPOINT ["dotnet", "Huatek.Torch.Promotion.API.dll"]