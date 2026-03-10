FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["WEB/WEB.csproj", "WEB/"]
RUN dotnet restore "WEB/WEB.csproj"
COPY . .
WORKDIR "/src/WEB"

FROM build AS publish
RUN dotnet publish "WEB.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

COPY --from=publish /app/publish/wwwroot .

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]