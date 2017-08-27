using prodrink.catalog;

namespace prodrink.gateway.Services.Grpc
{
    public class CatalogProvider : BaseGrpcProvider
    {
        public CatalogProvider(ISecretsStorageService secretsService) : base(secretsService)
        {
            Client = new CatalogService.CatalogServiceClient(Channel);
        }

        protected override string ServiceKey => "CATALOG_SERVICE_HOST";

        public CatalogService.CatalogServiceClient Client { get; }
    }
}