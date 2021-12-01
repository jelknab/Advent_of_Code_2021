FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Advent of Code 2021/Advent of Code 2021.csproj", "Advent of Code 2021/"]
RUN dotnet restore "Advent of Code 2021/Advent of Code 2021.csproj"
COPY . .
WORKDIR "/src/Advent of Code 2021"
RUN dotnet build "Advent of Code 2021.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Advent of Code 2021.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Advent of Code 2021.dll"]
