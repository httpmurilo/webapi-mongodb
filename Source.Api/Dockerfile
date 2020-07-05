
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
COPY bin/Release/netcoreapp3.1/publish/ App/
WORKDIR /App
EXPOSE 5000/tcp
ENTRYPOINT ["dotnet", "run", "--server.urls", "http://*:5000"]