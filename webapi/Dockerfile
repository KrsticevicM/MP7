FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore webapi
RUN dotnet restore reactapp
# Build and publish a release
RUN dotnet publish webapi -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App

EXPOSE 5000/tcp

ENV ASPNETCORE_URLS http://*:5000

COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "webapi.dll"]