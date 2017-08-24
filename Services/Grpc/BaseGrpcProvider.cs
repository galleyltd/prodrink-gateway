using System;
using Grpc.Core;
using Microsoft.Extensions.Configuration;

namespace prodrink.gateway.Services.Grpc
{
    public abstract class BaseGrpcProvider
    {
        private readonly IConfiguration _configuration;
        protected Channel Channel;

        protected BaseGrpcProvider(IConfiguration configuration)
        {
            _configuration = configuration;
            Init();
        }

        private void Init()
        {
            Channel = new Channel(ServiceAddress, ChannelCredentials.Insecure);
        }

        protected abstract string ServiceKey { get; }

        private string ServiceAddress =>
            $"{GetValueFromEnvOrConfig(ServiceKey, "Url")}:{GetValueFromEnvOrConfig(ServiceKey, "Port")}";

        private string GetValueFromEnvOrConfig(string serviceKey, string valueKey)
        {
            var envVariable = Environment.GetEnvironmentVariable(string.Concat(serviceKey, valueKey));
            return string.IsNullOrEmpty(envVariable)
                ? _configuration.GetValue<string>($"Grpc:Services:{serviceKey}:{valueKey}")
                : envVariable;
        }
    }
}