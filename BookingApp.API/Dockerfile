#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
RUN pwd && ls #debug file path and files
COPY ../*.sln ./
COPY ../*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done
RUN dotnet restore "BookingApp.API/BookingApp.API.csproj"
COPY . .
WORKDIR "/src/BookingApp.API"
RUN dotnet build "BookingApp.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookingApp.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookingApp.API.dll"]