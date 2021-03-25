# FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
# WORKDIR /app
# COPY . .

# CMD ASPNETCORE_URLS=http://*:$PORT dotnet Grouper.Api.Web.dll



FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
RUN mkdir /DockerSource
WORKDIR /DockerSource

COPY . /DockerSource

RUN dotnet restore
RUN dotnet publish -c release -o /DockerOutput/Website --no-restore

# FROM mcr.microsoft.com/dotnet/aspnet:5.0
# RUN mkdir /DockerOutput/Website
# WORKDIR /DockerOutput/Website
# COPY --from=build /DockerOutput/Website ./

# ENTRYPOINT ["dotnet", "Grouper.Api.Web.dll"]