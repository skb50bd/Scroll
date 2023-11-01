FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /source

COPY Scroll.sln /source/
COPY src/Domain/Scroll.Domain.csproj /source/src/Domain/
COPY src/Data/Scroll.Data.csproj /source/src/Data/
COPY src/Core/Scroll.Core.csproj /source/src/Core/
COPY src/Api/Scroll.Api.csproj /source/src/Api/
COPY src/Web/Scroll.Web.csproj /source/src/Web/
COPY tests/Scroll.Tests/Scroll.Tests.csproj /source/tests/Scroll.Tests/
RUN dotnet restore

COPY . .
WORKDIR /source/src/Api
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine as final
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "./Scroll.Api.dll"]
