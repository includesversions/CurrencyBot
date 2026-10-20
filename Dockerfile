FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY . .

RUN dotnet publish CurrencyBot.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/runtime:10.0

WORKDIR /app

COPY --from=build /app/out .

CMD ["dotnet", "CurrencyBot.dll"]
