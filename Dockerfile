# Stage: runtime for ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Stage: build your .NET application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MyFirstWebApp.csproj", "."]
RUN dotnet restore "./MyFirstWebApp.csproj"
COPY . .
RUN dotnet build "./MyFirstWebApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage: Build Tailwind CSS using Node.js
FROM node:18-alpine AS tailwind
WORKDIR /src
# Copy Node-related files (adjust if they live in a subfolder)
COPY package*.json tailwind.config.js ./
RUN npm install
# Copy the rest of your files needed for Tailwind processing
COPY . .
RUN npx tailwindcss -i ./wwwroot/css/input.css -o ./wwwroot/css/tailwind.css --minify

# Stage: Publish .NET application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MyFirstWebApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
# Copy generated Tailwind CSS into the publish folder
COPY --from=tailwind /src/wwwroot/css/tailwind.css /app/publish/wwwroot/css/tailwind.css

# Stage: Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyFirstWebApp.dll"]