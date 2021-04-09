
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Juros.Api/Juros.Api.csproj", "Services/Juros.Api/"]
RUN dotnet restore "Services/Juros.Api/Juros.Api.csproj"
COPY . .
WORKDIR "/src/Services/Juros.Api"
RUN dotnet build "Juros.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Juros.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Juros.Api.dll"]
