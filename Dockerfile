FROM microsoft/aspnetcore
WORKDIR /app
COPY out .
EXPOSE 80
RUN chmod +x ./docker-entrypoint.sh
CMD /bin/bash ./docker-entrypoint.sh