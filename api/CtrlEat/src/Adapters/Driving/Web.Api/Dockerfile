#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Adapters/Driving/Web.Api/Web.Api.csproj", "src/Adapters/Driving/Web.Api/"]
COPY ["src/Adapters/Driven/Persistence/Persistence.csproj", "src/Adapters/Driven/Persistence/"]
RUN dotnet restore "src/Adapters/Driving/Web.Api/Web.Api.csproj"
COPY . .
WORKDIR "/src/src/Adapters/Driving/Web.Api"
RUN dotnet build "Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.Api.dll"]