# syntax=docker/dockerfile-upstream:master-labs
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /source

COPY ./global.json ./
COPY --parents ./**/*.fsproj ./
COPY --parents ./**/*.csproj ./

RUN --mount=type=cache,target=/root/.nuget/packages \
    dotnet restore /source/src/Api/Scroll.Api.csproj

COPY . .
WORKDIR /source/src/Api
RUN --mount=type=cache,target=/root/.nuget/packages \
    dotnet publish /source/src/Api/Scroll.Api.csproj -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine as final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["/app/Scroll.Api"]
