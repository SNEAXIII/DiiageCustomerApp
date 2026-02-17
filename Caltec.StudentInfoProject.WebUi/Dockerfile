# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN addgroup --system appgroup && adduser --system --ingroup appgroup appuser

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY --chmod=444 ["Caltech.StudentInfoProject.sln", "./"]
COPY --chmod=444 ["Caltec.StudentInfoProject.WebUi/Caltec.StudentInfoProject.WebUi.csproj", "Caltec.StudentInfoProject.WebUi/"]
COPY --chmod=444 ["Caltec.StudentInfoProject.Business/Caltec.StudentInfoProject.Business.csproj", "Caltec.StudentInfoProject.Business/"]
COPY --chmod=444 ["Caltec.StudentInfoProject.Persistence/Caltec.StudentInfoProject.Persistence.csproj", "Caltec.StudentInfoProject.Persistence/"]
COPY --chmod=444 ["Caltec.StudentInfoProject.Domain/Caltec.StudentInfoProject.Domain.csproj", "Caltec.StudentInfoProject.Domain/"]
COPY --chmod=444 ["Caltec.StudentInfoProject.Common/Caltec.StudentInfoProject.Common.csproj", "Caltec.StudentInfoProject.Common/"]
RUN dotnet restore "./Caltec.StudentInfoProject.WebUi/Caltec.StudentInfoProject.WebUi.csproj"
COPY --chmod=555 ["Caltec.StudentInfoProject.WebUi/", "Caltec.StudentInfoProject.WebUi/"]
COPY --chmod=555 ["Caltec.StudentInfoProject.Business/", "Caltec.StudentInfoProject.Business/"]
COPY --chmod=555 ["Caltec.StudentInfoProject.Persistence/", "Caltec.StudentInfoProject.Persistence/"]
COPY --chmod=555 ["Caltec.StudentInfoProject.Domain/", "Caltec.StudentInfoProject.Domain/"]
COPY --chmod=555 ["Caltec.StudentInfoProject.Common/", "Caltec.StudentInfoProject.Common/"]
WORKDIR "/src/Caltec.StudentInfoProject.WebUi"
RUN dotnet build "./Caltec.StudentInfoProject.WebUi.csproj" \
    -c "$BUILD_CONFIGURATION" \
    -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Caltec.StudentInfoProject.WebUi.csproj" \
    -c "$BUILD_CONFIGURATION" \
    -o /app/publish \
    /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish --chown=root:root --chmod=555 /app/publish .
USER appuser
ENTRYPOINT ["dotnet", "Caltec.StudentInfoProject.WebUi.dll"]
