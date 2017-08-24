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

        protected override string ServiceKey => "Catalog";

        public CatalogService.CatalogServiceClient Client { get; }
    }
}