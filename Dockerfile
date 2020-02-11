# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY BackgroundJob/*.csproj ./BackgroundJob/
RUN dotnet restore -r win-x64

# copy everything else and build app
COPY BackgroundJob/. ./BackgroundJob/
WORKDIR /source/BackgroundJob/
RUN dotnet publish -c release -o /app

# final stage/image
# Uses the 1909 release; 1903, and 1809 are other choices
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1809 AS runtime
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["BackgroundJob"]
