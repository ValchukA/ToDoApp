FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["backend/Directory.Build.props", "."]
COPY ["backend/ToDoApp.Api/", "ToDoApp.Api/"]
COPY ["backend/ToDoApp.Bll/", "ToDoApp.Bll/"]
COPY ["backend/ToDoApp.Common/", "ToDoApp.Common/"]
COPY ["backend/ToDoApp.Storage.MongoDb/", "ToDoApp.Storage.MongoDb/"]
RUN dotnet restore "ToDoApp.Api/ToDoApp.Api.csproj"
WORKDIR "/src/ToDoApp.Api"
RUN dotnet build "ToDoApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToDoApp.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDoApp.Api.dll"]
