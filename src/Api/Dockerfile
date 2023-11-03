FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /source

COPY src/Common/Scroll.Common.csproj /source/src/Common/
COPY src/Domain/Scroll.Domain.csproj /source/src/Domain/
COPY src/Data/Scroll.Data.csproj /source/src/Data/
COPY src/Core/Scroll.Core.csproj /source/src/Core/
COPY src/Api/Scroll.Api.csproj /source/src/Api/
COPY tests/Scroll.Tests/Scroll.Tests.csproj /source/tests/Scroll.Tests/
WORKDIR /source/src/Api
RUN dotnet restore

WORKDIR /source
COPY . .
WORKDIR /source/src/Api
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine as final
# TODO: Remove this when we add support for non-english languages
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "./Scroll.Api.dll"]
