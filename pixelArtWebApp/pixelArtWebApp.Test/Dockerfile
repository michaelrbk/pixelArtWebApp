#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base
WORKDIR /app
COPY . .
RUN dotnet test

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["pixelArtWebApp.Test/pixelArtWebApp.Test.csproj", "pixelArtWebApp.Test/"]
COPY ["pixelArtWebApp/pixelArtWebApp.csproj", "pixelArtWebApp/"]
RUN dotnet restore "pixelArtWebApp.Test/pixelArtWebApp.Test.csproj"
COPY . .
WORKDIR "/src/pixelArtWebApp.Test"
RUN dotnet build "pixelArtWebApp.Test.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "pixelArtWebApp.Test.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "pixelArtWebApp.Test.dll"]