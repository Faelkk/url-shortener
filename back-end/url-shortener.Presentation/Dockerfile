FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar os arquivos .csproj de todos os projetos referenciados
COPY ./url-shortener.Presentation/*.csproj ./url-shortener.Presentation/
COPY ./url-shortener.Application/*.csproj ./url-shortener.Application/
COPY ./url-shortener.Infrastructure/*.csproj ./url-shortener.Infrastructure/
COPY ./url-shortener.Domain/*.csproj ./url-shortener.Domain/

# Restaurar dependências
RUN dotnet restore ./url-shortener.Presentation/url-shortener.Presentation.csproj

# Copiar todo o código fonte (de todos os projetos)
COPY . .

# Publicar o projeto Presentation
RUN dotnet publish ./url-shortener.Presentation/url-shortener.Presentation.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "url-shortener.Presentation.dll"]
