FROM microsoft/aspnetcore
WORKDIR /app
COPY out .
ENV ASPNETCORE_URLS http://*:80
EXPOSE 80
ENTRYPOINT ["dotnet", "prodrink.gateway.dll"]