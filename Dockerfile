# build .NET app:
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS buildnet
WORKDIR /app

COPY *.sln .
COPY . ./

RUN dotnet restore 

WORKDIR /app/Lilab.Api
RUN dotnet publish Lilab.Api.csproj -c Release -o published

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT Production
ENV ASPNETCORE_URLS http://+:$PORT
EXPOSE $PORT
# does not contain the trailing slash
# ENV CORS_DOMAIN http://localhost:3000

# copy .net content
COPY --from=buildnet /app/Lilab.Api/published ./

# ENTRYPOINT ["dotnet", "Lilab.Api.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Lilab.Api.dll
