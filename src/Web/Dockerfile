FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
RUN dotnet workload install wasm-tools
RUN apk update && apk add --no-cache libc++ mono-complete
WORKDIR /source

COPY /src/Web/Scroll.Web.csproj ./src/Web/
RUN dotnet restore ./src/Web/Scroll.Web.csproj

COPY . .
WORKDIR /source/src/Web
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY --from=build /source/src/Web/nginx.conf /etc/nginx/nginx.conf
