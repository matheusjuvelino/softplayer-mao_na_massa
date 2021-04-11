
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Juros/TaxaDeJuros/TaxaDeJuros.Api/TaxaDeJuros.Api.csproj", "Services/Juros/TaxaDeJuros/TaxaDeJuros.Api/TaxaDeJuros.Api.csproj"]
RUN dotnet restore "Services/Juros/TaxaDeJuros/TaxaDeJuros.Api/TaxaDeJuros.Api.csproj"
COPY . .
WORKDIR "/src/Services/Juros/TaxaDeJuros/TaxaDeJuros.Api"
RUN dotnet build "TaxaDeJuros.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaxaDeJuros.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaxaDeJuros.Api.dll"]
