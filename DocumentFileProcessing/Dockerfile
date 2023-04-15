#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DocumentFileProcessing/DocumentFileProcessing.csproj", "DocumentFileProcessing/"]
COPY ["DocumentFileProcessing.Data/DocumentFileProcessing.Data.csproj", "DocumentFileProcessing.Data/"]
COPY ["DocumentFileProcessing.DTO/DocumentFileProcessing.DTO.csproj", "DocumentFileProcessing.DTO/"]
COPY ["DocumentFileProcessing.Repo/DocumentFileProcessing.Repo.csproj", "DocumentFileProcessing.Repo/"]
COPY ["DocumentFileProcessing.Service/DocumentFileProcessing.Service.csproj", "DocumentFileProcessing.Service/"]
RUN dotnet restore "DocumentFileProcessing/DocumentFileProcessing.csproj"
COPY . .
WORKDIR "/src/DocumentFileProcessing"
RUN dotnet build "DocumentFileProcessing.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DocumentFileProcessing.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DocumentFileProcessing.dll"]