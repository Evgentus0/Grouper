# FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
# WORKDIR /app
# COPY . .

# CMD ASPNETCORE_URLS=http://*:$PORT dotnet Grouper.Api.Web.dll



FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

RUN mkdir /DockerSource
WORKDIR /DockerSource

COPY . /DockerSource

RUN dotnet restore
RUN dotnet publish -c release -o /DockerOutput --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
RUN mkdir /DockerOutput
WORKDIR /DockerOutput
COPY --from=build /DockerOutput ./

CMD ASPNETCORE_URLS=http://*:$PORT dotnet Grouper.Api.Web.dll