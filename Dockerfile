FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NLMK/NLMK.csproj", "NLMK/"]
RUN dotnet restore "NLMK/NLMK.csproj"
COPY . .
WORKDIR "/src/NLMK"
RUN dotnet build "NLMK.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NLMK.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NLMK.dll"]
