FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ClubestApp/ClubestApp.csproj", "ClubestApp/"]
RUN dotnet restore "ClubestApp/ClubestApp.csproj"
COPY . .
WORKDIR "/src/ClubestApp"
RUN dotnet build "ClubestApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClubestApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClubestApp.dll"]