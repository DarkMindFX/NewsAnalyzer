FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
COPY bin/Release/netcoreapp3.1/ API/
WORKDIR /API
ENV ASPNETCORE_URLS=http://+:8089
ENTRYPOINT ["dotnet", "API.dll"]