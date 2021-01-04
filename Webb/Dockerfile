#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Webb/Webb.csproj", "Webb/"]
RUN dotnet restore "Webb/Webb.csproj"
COPY . .
WORKDIR "/src/Webb"
RUN dotnet build "Webb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Webb.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "run", "--server.urls", "http://*:5000"]
#ENTRYPOINT ["dotnet", "Webb.dll"] Like this when compose is done