
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["TaskManager.API/TaskManager.API.csproj", "TaskManager.API/"]
RUN dotnet restore "TaskManager.API/TaskManager.API.csproj"

COPY . .
WORKDIR "/src/TaskManager.API"
RUN dotnet publish "TaskManager.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Docker

ENTRYPOINT ["dotnet", "TaskManager.API.dll"]
