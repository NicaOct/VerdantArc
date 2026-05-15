FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["VerdantArc.Web/VerdantArc.Web.csproj", "VerdantArc.Web/"]
COPY ["VerdantArc.Application/VerdantArc.Application.csproj", "VerdantArc.Application/"]
COPY ["VerdantArc.Infrastructure/VerdantArc.Infrastructure.csproj", "VerdantArc.Infrastructure/"]
COPY ["VerdantArc.Domain/VerdantArc.Domain.csproj", "VerdantArc.Domain/"]
RUN dotnet restore "VerdantArc.Web/VerdantArc.Web.csproj"
COPY . .
WORKDIR "/src/VerdantArc.Web"
RUN dotnet build "VerdantArc.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VerdantArc.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VerdantArc.Web.dll"]
