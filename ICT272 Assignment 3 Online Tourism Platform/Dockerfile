FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ICT272 Assignment 3 Online Tourism Platform/ICT272 Assignment 3 Online Tourism Platform.csproj", "ICT272 Assignment 3 Online Tourism Platform/"]
RUN dotnet restore "ICT272 Assignment 3 Online Tourism Platform/ICT272 Assignment 3 Online Tourism Platform.csproj"
COPY . .
WORKDIR "/src/ICT272 Assignment 3 Online Tourism Platform"
RUN dotnet build "ICT272 Assignment 3 Online Tourism Platform.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ICT272 Assignment 3 Online Tourism Platform.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ICT272 Assignment 3 Online Tourism Platform.dll"]
