FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ../ModelsLibrary ./ModelsLibrary
COPY . .
RUN dotnet restore "PostApi/PostApi.csproj" --disable-parallel
RUN dotnet publish "PostApi/PostApi.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5073
ENTRYPOINT ["dotnet", "PostApi.dll"]
