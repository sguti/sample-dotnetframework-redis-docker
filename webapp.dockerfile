FROM        mcr.microsoft.com/dotnet/framework/sdk:latest AS build

LABEL       Author="sguti"

ARG         configuration=Release

ENV         mode=${configuration}

WORKDIR     /app

# copy csproj and restore as distinct layers
COPY        *.sln .
COPY        RCNETAPI/*.csproj ./RCNETAPI/
COPY        RCNETAPI/*.config ./RCNETAPI/

RUN         nuget restore

# copy everything else and build app
COPY        RCNETAPI/. ./RCNETAPI/

WORKDIR     /app/RCNETAPI

RUN         msbuild /p:Configuration=${mode} -r:False

# FROM        mcr.microsoft.com/windows/servercore/iis:windowsservercore-ltsc2019 AS runtime
FROM        mcr.microsoft.com/dotnet/framework/aspnet:4.8-windowsservercore-ltsc2019 as runtime

WORKDIR     /inetpub/wwwroot

COPY        --from=build /app/RCNETAPI/. ./