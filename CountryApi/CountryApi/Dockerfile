FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src
RUN apt update && apt install -y nodejs

COPY ["CountryApi/*.csproj", "CountryApi/"]

RUN dotnet restore "CountryApi/CountryApi.csproj"
COPY . .

WORKDIR "/src/CountryApi"
RUN dotnet build "CountryApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CountryApi.csproj" -c Release -o /app

# Final stage - Create a smaller image by copying the output from the publish stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app .

EXPOSE 80
ENTRYPOINT ["dotnet", "CountryApi.dll"] 