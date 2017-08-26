#!/bin/bash
set -ev

DOCKER_USERNAME=$1
DOCKER_PASSWORD=$2

PROJECT_NAME="prodrink/prodrink-gateway"

# Login to Docker Hub and upload images
docker login -u="$DOCKER_USERNAME" -p="$DOCKER_PASSWORD"
docker push ${PROJECT_NAME}:latest
