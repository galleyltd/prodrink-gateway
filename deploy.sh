#!/bin/bash
set -ev

TAG=$1
DOCKER_USERNAME=$2
DOCKER_PASSWORD=$3

PROJECT_NAME="prodrink/prodrink-gateway"

# Create publish artifact
dotnet publish -c Release -o out

# Build the Docker images
docker build -t ${PROJECT_NAME}:${TAG} .
docker tag ${PROJECT_NAME}:${TAG} ${PROJECT_NAME}:latest

# Login to Docker Hub and upload images
docker login -u="$DOCKER_USERNAME" -p="$DOCKER_PASSWORD"
docker push ${PROJECT_NAME}:${TAG}
docker push ${PROJECT_NAME}:latest
