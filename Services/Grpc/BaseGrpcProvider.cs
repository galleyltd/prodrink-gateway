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
            Channel = new Channel(GetServiceUrl(ServiceUrlKey), ChannelCredentials.Insecure);
        }

        protected abstract string ServiceUrlKey { get; }

        private string GetServiceUrl(string key)
        {
            return _configuration.GetValue<string>(key);
        }
    }
}