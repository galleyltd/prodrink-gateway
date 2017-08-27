using System;
using StackExchange.Redis;

namespace prodrink.gateway.Services
{
    public class SecretsStorageService : ISecretsStorageService
    {
        private ConnectionMultiplexer _redis;

        private const string PostgresHostKey = "POSTGRES_HOST";
        private const string DefaultHost = "localhost";

        private const string SecretsRedisHostKey = "SECRETS_REDIS_HOST";
        private const string SecretsRedisPortKey = "SECRETS_REDIS_PORT";
        private const string SecretsRedisPasswordKey = "SECRETS_REDIS_PASSWORD";

        public string GetPostgresHost()
        {
            return Environment.GetEnvironmentVariable(PostgresHostKey) ?? DefaultHost;
        }

        public string GetGoogleAuthClientId() => GetSecretFromRedisByKey("auth:google:clientid");

        public string GetGoogleAuthClientSecret() => GetSecretFromRedisByKey("auth:google:clientsecret");

        public string GetGrpcServiceHost(string serviceKey)
        {
            return Environment.GetEnvironmentVariable(serviceKey) ?? DefaultHost;
        }

        private void InitRedisConnection()
        {
            var redisHost = Environment.GetEnvironmentVariable(SecretsRedisHostKey);
            var redisPort = Environment.GetEnvironmentVariable(SecretsRedisPortKey);
            var redisPassword = Environment.GetEnvironmentVariable(SecretsRedisPasswordKey);

            var options = new ConfigurationOptions
            {
                EndPoints = {{redisHost, int.Parse(redisPort)}},
                Password = redisPassword
            };
            _redis = ConnectionMultiplexer.Connect(options);
        }

        private string GetSecretFromRedisByKey(string secretKey)
        {
            if (_redis == null) InitRedisConnection();
            var database = _redis.GetDatabase();
            return database.StringGet(secretKey);
        }
    }
}