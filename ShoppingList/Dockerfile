#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5432

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ShoppingList/ShoppingList.csproj", "ShoppingList/"]
RUN dotnet restore "ShoppingList/ShoppingList.csproj"
COPY . .
WORKDIR "/src/ShoppingList"
RUN dotnet build "ShoppingList.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShoppingList.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShoppingList.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY published/ ./
ENTRYPOINT ["dotnet", "aspnetapp.dll"]

#Postgre
FROM postgres:14.5-bullseye

# Install software
RUN apt update --fix-missing
RUN apt install -y postgresql-14-postgis-3

# Set locale
RUN localedef -i en_GB -c -f UTF-8 -A /usr/share/locale/locale.alias en_GB.UTF-8
ENV LANG en_GB.utf8