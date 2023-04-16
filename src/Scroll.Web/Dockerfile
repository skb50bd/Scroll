FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0-preview-alpine AS build
WORKDIR /src
COPY ["src/Scroll.Web/Scroll.Web.csproj", "src/Scroll.Web/"]
COPY ["src/Scroll.Service/Scroll.Service.csproj", "src/Scroll.Service/"]
COPY ["src/Scroll.Data/Scroll.Data.csproj", "src/Scroll.Data/"]
COPY ["src/Scroll.Library/Scroll.Library.csproj", "src/Scroll.Library/"]
RUN dotnet restore "src/Scroll.Web/Scroll.Web.csproj"
COPY . .
WORKDIR "/src/src/Scroll.Web"
RUN dotnet build "Scroll.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Scroll.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Scroll.Web.dll"]
