FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
#COPY ["Life_Healthy_API/Life_Healthy_API.csproj", "Life_Healthy_API/"]
#RUN dotnet restore "Life_Healthy_API/Life_Healthy_API.csproj"
COPY . .
#WORKDIR "/src/Life_Healthy_API"
RUN dotnet restore
RUN dotnet build "Life_Healthy_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Life_Healthy_API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Life_Healthy_API.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Life.dll