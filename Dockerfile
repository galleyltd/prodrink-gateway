FROM microsoft/aspnetcore
WORKDIR /app
COPY out .
COPY docker-entrypoint.sh .
EXPOSE 80
RUN chmod +x ./docker-entrypoint.sh
CMD /bin/bash ./docker-entrypoint.sh