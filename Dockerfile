FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /
EXPOSE 5001
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS restore
WORKDIR /app
COPY ./*.sln ./
COPY src/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p src/${file%.*}/ && mv $file src/${file%.*}/; done
COPY tests/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p tests/${file%.*}/ && mv $file tests/${file%.*}/; done
RUN dotnet restore "Challenge.sln"
COPY . .

FROM restore AS publish
WORKDIR "/app/src/Challenge.Api"
RUN dotnet publish "Challenge.Api.csproj" -c Release -o /out

FROM restore AS Unit-tests
WORKDIR "/app/tests/Challenge.UnitTests"
RUN dotnet test --logger:trx --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /out .
ENTRYPOINT ["dotnet", "Challenge.Api.dll"]