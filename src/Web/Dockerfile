# syntax=docker/dockerfile-upstream:master-labs
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /source

COPY ./global.json ./
COPY --parents ./**/*.fsproj ./
COPY --parents ./**/*.csproj ./

RUN --mount=type=cache,target=/root/.nuget/packages \
    dotnet restore /source/src/Web/Scroll.Web.csproj --use-current-runtime

COPY ./src/Common/     ./src/Common
COPY ./src/Domain/     ./src/Domain/
COPY ./src/Core/       ./src/Core/
COPY ./src/Data/       ./src/Data/
COPY ./src/Web.Client/ ./src/Web.Client/
COPY ./src/Web/        ./src/Web/

from build as develop
WORKDIR /source/src/Web
RUN --mount=type=cache,target=/root/.nuget/packages \
    dotnet watch run --no-restore

from build as publish
RUN --mount=type=cache,target=/root/.nuget/packages \
    dotnet publish /source/src/Web/Scroll.Web.csproj \
        -o /app \
        --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine as final
RUN apk add --no-cache curl
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["/app/Scroll.Web"]
