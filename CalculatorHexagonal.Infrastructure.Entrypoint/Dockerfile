FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

COPY bin/Debug/net8.0/  .


ENV TZ=America/Bogota
RUN sh -c "ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone"

