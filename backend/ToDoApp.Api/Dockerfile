FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY backend/ToDoApp.Api ToDoApp.Api
COPY backend/ToDoApp.Bll ToDoApp.Bll
COPY backend/ToDoApp.Storage.MongoDb ToDoApp.Storage.MongoDb
COPY backend/Directory.Build.props .
RUN dotnet publish ToDoApp.Api -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "ToDoApp.Api.dll"]
