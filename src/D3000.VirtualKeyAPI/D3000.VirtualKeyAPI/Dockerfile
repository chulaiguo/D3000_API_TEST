#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["D3000.VirtualKeyAPI/D3000.VirtualKeyAPI.csproj", "D3000.VirtualKeyAPI/"]
RUN dotnet restore "D3000.VirtualKeyAPI/D3000.VirtualKeyAPI.csproj"
COPY . .
WORKDIR "/src/D3000.VirtualKeyAPI"
RUN dotnet build "D3000.VirtualKeyAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "D3000.VirtualKeyAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "D3000.VirtualKeyAPI.dll"]