FROM microsoft/aspnetcore-build AS BUILD_IMAGE
WORKDIR /app
COPY *.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /out/

FROM microsoft/aspnetcore
WORKDIR /app
COPY --from=BUILD_IMAGE out .
COPY docker-entrypoint.sh .
EXPOSE 80
RUN chmod +x ./docker-entrypoint.sh
CMD /bin/bash ./docker-entrypoint.sh