FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["StudentTransfer.Api/StudentTransfer.Api.csproj", "StudentTransfer.Api/"]
COPY ["StudentTransfer.Dal/StudentTransfer.Dal.csproj", "StudentTransfer.Dal/"]
COPY ["StudentTransfer.Bll/StudentTransfer.Bll.csproj", "StudentTransfer.Bll/"]
COPY ["StudentTransfer.Utils/StudentTransfer.Utils.csproj", "StudentTransfer.Utils/"]
COPY ["StudentTransfer.VacantParser/StudentTransfer.VacantParser.csproj", "StudentTransfer.VacantParser/"]
RUN dotnet restore "StudentTransfer.Api/StudentTransfer.Api.csproj"
COPY . .
WORKDIR "/src/StudentTransfer.Api"
RUN dotnet build "StudentTransfer.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StudentTransfer.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudentTransfer.Api.dll"]
