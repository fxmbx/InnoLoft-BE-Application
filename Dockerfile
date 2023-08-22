FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app

EXPOSE 5177
ENV ASPNETCORE_URLS=http://+:5177

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

COPY ["EventModuleApi/EventModuleApi.csproj", "EventModuleApi/"]
RUN dotnet restore "EventModuleApi/EventModuleApi.csproj"

COPY . .
WORKDIR "/src/EventModuleApi"
RUN dotnet build "./EventModuleApi.csproj" -c Release -o /app/build
RUN dotnet tool install --global dotnet-ef

FROM build AS publish
RUN dotnet publish "./EventModuleApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app

COPY --from=publish /app/publish .

COPY wait-for-it.sh /app/wait-for-it.sh
RUN chmod +x /app/wait-for-it.sh

WORKDIR /app

COPY entrypoint.sh /app/entrypoint.sh
RUN chmod +x /app/entrypoint.sh

ENV ConnectionStrings__DefaultConnection="Server=mysql;Database=innoloft_db;User=innoloft;Password=innoloftpass"

ENTRYPOINT ["/app/entrypoint.sh"]
