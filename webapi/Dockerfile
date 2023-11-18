FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
COPY . ./
EXPOSE 7024/tcp
ENV ASPNETCORE_URLS  https://*:7024
ENTRYPOINT ["dotnet", "run"]