
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Juros/CalcularJuros/CalcularJuros.Api/CalcularJuros.Api.csproj", "Services/Juros/CalcularJuros/CalcularJuros.Api/CalcularJuros.Api.csproj"]
RUN dotnet restore "Services/Juros/CalcularJuros/CalcularJuros.Api/CalcularJuros.Api.csproj"
COPY . .
WORKDIR "/src/Services/Juros/CalcularJuros/CalcularJuros.Api"
RUN dotnet build "CalcularJuros.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CalcularJuros.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CalcularJuros.Api.dll"]
