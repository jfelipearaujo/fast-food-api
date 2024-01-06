FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

ENV DB_NAME=""
ENV DB_HOST=""
ENV DB_PORT=""
ENV DB_USER=""
ENV DB_PASS=""

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Adapters/Driving/Web.Api/Web.Api.csproj", "src/Adapters/Driving/Web.Api/"]
COPY ["src/Core/Application/Application.csproj", "src/Core/Application/"]
COPY ["src/Core/Domain/Domain.csproj", "src/Core/Domain/"]
COPY ["src/Adapters/Driven/Infrastructure/Infrastructure.csproj", "src/Adapters/Driven/Infrastructure/"]
COPY ["src/Adapters/Driven/Persistence/Persistence.csproj", "src/Adapters/Driven/Persistence/"]
RUN dotnet restore "src/Adapters/Driving/Web.Api/Web.Api.csproj"
COPY . .
WORKDIR "/src/src/Adapters/Driving/Web.Api"
RUN dotnet build "Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
RUN mkdir images
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.Api.dll"]