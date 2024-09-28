FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY *.sln ./
COPY MarketManagement/*.csproj ./MarketManagement/
COPY MarketManagement.Infrastructure/*.csproj ./MarketManagement.Infrastructure/
COPY MaretManagement.Domain/*.csproj ./MaretManagement.Domain/
COPY MarketManagement.UnitTests/*.csproj ./MarketManagement.UnitTests/

RUN dotnet restore

COPY . .

RUN dotnet build -c Release --no-restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "MarketManagement.dll"]
