ARG SDKARC=amd64
FROM mcr.microsoft.com/dotnet/aspnet:8.0.3-bookworm-slim-amd64 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0.203-bookworm-slim-arm64v8 AS build
ARG BUILD_CONFIGURATION=Release


WORKDIR /src
COPY ["BlazorCourse/BlazorCourse.csproj", "BlazorCourse/"]
RUN dotnet restore "BlazorCourse/BlazorCourse.csproj"
COPY . .
WORKDIR "/src/BlazorCourse"
RUN dotnet build "BlazorCourse.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BlazorCourse.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
#VOLUME /app
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorCourse.dll"]
