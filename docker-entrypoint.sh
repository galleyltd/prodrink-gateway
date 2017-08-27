#!/bin/bash

set -e

until dotnet ef database update; do
    >&2 echo "Database is starting up"
    sleep 1
done

>&2 echo "Database is up - executing command"
dotnet run --server.urls http://*:80