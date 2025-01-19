FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ../ModelsLibrary ./ModelsLibrary
COPY . .
RUN dotnet restore "IdentityApi/IdentityApi.csproj" --disable-parallel
RUN dotnet publish "IdentityApi/IdentityApi.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000
ENTRYPOINT ["dotnet", "IdentityApi.dll"]