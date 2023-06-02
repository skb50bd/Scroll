FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Scroll.Rest/Scroll.Rest.csproj", "src/Scroll.Rest/"]
RUN dotnet restore "src/Scroll.Rest/Scroll.Rest.csproj"
COPY . .
WORKDIR "/src/src/Scroll.Rest"
RUN dotnet build "Scroll.Rest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Scroll.Rest.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Scroll.Rest.dll"]
