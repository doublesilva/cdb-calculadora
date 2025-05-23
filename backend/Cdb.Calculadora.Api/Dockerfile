FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia tudo da raiz do projeto (onde está a .sln e as pastas backend/frontend)
COPY . .

# Restaura a solution (com caminho relativo correto)
RUN dotnet restore "Cdb.Calculadora.sln"

# Publica a aplicação
RUN dotnet publish "backend/Cdb.Calculadora.Api/Cdb.Calculadora.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Cdb.Calculadora.Api.dll"]
EXPOSE 80