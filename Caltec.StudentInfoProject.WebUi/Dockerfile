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
RUN addgroup --system appgroup && adduser --system --ingroup appgroup appuser
COPY --chown=appuser:appgroup ["Caltech.StudentInfoProject.sln", "./"]
COPY --chown=appuser:appgroup ["Caltec.StudentInfoProject.WebUi/Caltec.StudentInfoProject.WebUi.csproj", "Caltec.StudentInfoProject.WebUi/"]
COPY --chown=appuser:appgroup ["Caltec.StudentInfoProject.Business/Caltec.StudentInfoProject.Business.csproj", "Caltec.StudentInfoProject.Business/"]
COPY --chown=appuser:appgroup ["Caltec.StudentInfoProject.Persistence/Caltec.StudentInfoProject.Persistence.csproj", "Caltec.StudentInfoProject.Persistence/"]
COPY --chown=appuser:appgroup ["Caltec.StudentInfoProject.Domain/Caltec.StudentInfoProject.Domain.csproj", "Caltec.StudentInfoProject.Domain/"]
COPY --chown=appuser:appgroup ["Caltec.StudentInfoProject.Common/Caltec.StudentInfoProject.Common.csproj", "Caltec.StudentInfoProject.Common/"]
USER appuser
RUN dotnet restore "./Caltec.StudentInfoProject.WebUi/Caltec.StudentInfoProject.WebUi.csproj"
COPY --chown=appuser:appgroup ["Caltec.StudentInfoProject.WebUi/", "Caltec.StudentInfoProject.WebUi/"]
COPY --chown=appuser:appgroup ["Caltec.StudentInfoProject.Business/", "Caltec.StudentInfoProject.Business/"]
COPY --chown=appuser:appgroup ["Caltec.StudentInfoProject.Persistence/", "Caltec.StudentInfoProject.Persistence/"]
COPY --chown=appuser:appgroup ["Caltec.StudentInfoProject.Domain/", "Caltec.StudentInfoProject.Domain/"]
COPY --chown=appuser:appgroup ["Caltec.StudentInfoProject.Common/", "Caltec.StudentInfoProject.Common/"]
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
COPY --from=publish /app/publish .
USER appuser
ENTRYPOINT ["dotnet", "Caltec.StudentInfoProject.WebUi.dll"]
