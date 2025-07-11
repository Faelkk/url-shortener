FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY url-shortener.Presentation/url-shortener.Presentation.csproj ./
RUN dotnet restore "url-shortener.Presentation.csproj"

COPY url-shortener.Presentation/ ./
RUN dotnet publish "url-shortener.Presentation.csproj" -c release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY --from=build /app/out .


ENTRYPOINT [ "dotnet", "url-shortener.Presentation.dll" ]