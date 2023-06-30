#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ToDoApp.Api/ToDoApp.Api.csproj", "ToDoApp.Api/"]
RUN dotnet restore "ToDoApp.Api/ToDoApp.Api.csproj"
COPY . .
WORKDIR "/src/ToDoApp.Api"
RUN dotnet build "ToDoApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToDoApp.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDoApp.Api.dll"]