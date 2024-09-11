FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Lab01_ASP.NETCoreWebAPI/Lab01_ASP.NETCoreWebAPI.csproj", "Lab01_ASP.NETCoreWebAPI/"]
COPY ["BusinessObject/BusinessObject.csproj", "BusinessObject/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["Services/Services.csproj", "Services/"]
RUN dotnet restore "Lab01_ASP.NETCoreWebAPI/Lab01_ASP.NETCoreWebAPI.csproj"
COPY . .
WORKDIR "/src/Lab01_ASP.NETCoreWebAPI"
RUN dotnet build "Lab01_ASP.NETCoreWebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Lab01_ASP.NETCoreWebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lab01_ASP.NETCoreWebAPI.dll"]
