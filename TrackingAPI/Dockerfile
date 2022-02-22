FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /
COPY ["TrackingAPI/TrackingAPI.csproj", "TrackingAPI/"]
RUN dotnet restore "TrackingAPI/TrackingAPI.csproj"
COPY . .
WORKDIR "/TrackingAPI"
RUN dotnet build "TrackingAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TrackingAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TrackingAPI.dll"]
