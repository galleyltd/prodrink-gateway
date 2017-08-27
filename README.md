# prodrink-gateway 
[![Build Status](https://travis-ci.org/prodrink/prodrink-gateway.svg?branch=master)](https://travis-ci.org/prodrink/prodrink-gateway)
[![CodeFactor](https://www.codefactor.io/repository/github/prodrink/prodrink-gateway/badge)](https://www.codefactor.io/repository/github/prodrink/prodrink-gateway)

### How to launch locally:
```bash
docker run --name gateway-postgres -p 5432:5432 -e POSTGRES_PASSWORD=postgres -d postgres:9.6.4
docker run -d --name prodrink-gateway -p 80:80 \
    -e "POSTGRES_HOST=localhost" \
    -e "CATALOG_SERVICE_HOST=localhost" \
    -e "SECRETS_REDIS_HOST=localhost" \
    -e "SECRETS_REDIS_PORT=6379" \
    -e "SECRETS_REDIS_PASSWORD=password" \
    prodrink/prodrink-gateway
```