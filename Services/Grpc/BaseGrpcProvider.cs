using Grpc.Core;

namespace prodrink.gateway.Services.Grpc
{
    public abstract class BaseGrpcProvider
    {
        private readonly ISecretsStorageService _secretsService;
        protected Channel Channel;

        protected BaseGrpcProvider(ISecretsStorageService secretsService)
        {
            _secretsService = secretsService;
            Init();
        }

        private string ServiceAddress => _secretsService.GetGrpcServiceHost(ServiceKey);

        protected abstract string ServiceKey { get; }

        private void Init()
        {
            Channel = new Channel(ServiceAddress, ChannelCredentials.Insecure);
        }
    }
}