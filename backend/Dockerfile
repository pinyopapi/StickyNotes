FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY src/StickyNotes.Api/StickyNotes.Api.csproj src/StickyNotes.Api/
RUN dotnet restore "src/StickyNotes.Api/StickyNotes.Api.csproj"

COPY . .
WORKDIR "/app/src/StickyNotes.Api"
RUN dotnet publish "StickyNotes.Api.csproj" -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "StickyNotes.Api.dll"]