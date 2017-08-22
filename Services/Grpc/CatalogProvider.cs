using Microsoft.Extensions.Configuration;
using prodrink.catalog;

namespace prodrink.gateway.Services.Grpc
{
    public class CatalogProvider : BaseGrpcProvider
    {
        public CatalogProvider(IConfiguration configuration) : base(configuration)
        {
            Client = new CatalogService.CatalogServiceClient(Channel);
        }

        protected override string ServiceUrlKey => "CatalogServiceUrl";

        public CatalogService.CatalogServiceClient Client { get; }
    }
}