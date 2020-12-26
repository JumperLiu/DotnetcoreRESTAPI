FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DotnetcoreRESTAPI.csproj", "./"]
RUN dotnet restore "DotnetcoreRESTAPI.csproj"
COPY . .
RUN dotnet publish "DotnetcoreRESTAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DotnetcoreRESTAPI.dll"]
