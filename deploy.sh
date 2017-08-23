#!/bin/bash
set -ev

DOCKER_USERNAME=$1
DOCKER_PASSWORD=$2

PROJECT_NAME="prodrink/prodrink-gateway"

# Create publish artifact
dotnet publish -c Release -o out

# Build the Docker images
docker build -t ${PROJECT_NAME} .
docker tag ${PROJECT_NAME}:latest

# Login to Docker Hub and upload images
docker login -u="$DOCKER_USERNAME" -p="$DOCKER_PASSWORD"
docker push ${PROJECT_NAME}:latest
