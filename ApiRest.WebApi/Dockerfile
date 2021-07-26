#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ApiRest.WebApi/ApiRest.WebApi.csproj", "ApiRest.WebApi/"]
COPY ["ApiRest.Services/ApiRest.Services.csproj", "ApiRest.Services/"]
COPY ["ApiRest.Abstraction/ApiRest.Abstraction.csproj", "ApiRest.Abstraction/"]
COPY ["ApiRest.Entities/ApiRest.Entities.csproj", "ApiRest.Entities/"]
COPY ["ApiRest.Application/ApiRest.Application.csproj", "ApiRest.Application/"]
COPY ["ApiRest.Repository/ApiRest.Repository.csproj", "ApiRest.Repository/"]
COPY ["ApiRest.DataAccess/ApiRest.DataAccess.csproj", "ApiRest.DataAccess/"]
RUN dotnet restore "ApiRest.WebApi/ApiRest.WebApi.csproj"
COPY . .
WORKDIR "/src/ApiRest.WebApi"
RUN dotnet build "ApiRest.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiRest.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiRest.WebApi.dll"]