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
COPY ["src/Core/Application/Application.csproj", "src/Core/Application/"]
COPY ["src/Core/Domain/Domain.csproj", "src/Core/Domain/"]
COPY ["src/Core/Infrastructure/Infrastructure.csproj", "src/Core/Infrastructure/"]
COPY ["src/External/Persistence/Persistence.csproj", "src/External/Persistence/"]
COPY ["src/External/Web.Api/Web.Api.csproj", "src/External/Web.Api/"]
RUN dotnet restore "src/External/Web.Api/Web.Api.csproj"
COPY . .
WORKDIR "/src/src/External/Web.Api"
RUN dotnet build "Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
RUN mkdir images
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.Api.dll"]