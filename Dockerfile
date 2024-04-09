#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["HD2_EFDatabase.csproj", "."]
RUN dotnet restore "./.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "HD2_EFDatabase.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HD2_EFDatabase.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HD2_EFDatabase.dll"]