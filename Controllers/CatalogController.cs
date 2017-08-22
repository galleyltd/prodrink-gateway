using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core.Utils;
using Microsoft.AspNetCore.Mvc;
using prodrink.catalog;
using prodrink.gateway.Services.Grpc;

namespace prodrink.gateway.Controllers
{
    public class CatalogController : BaseGatewayController<CatalogProvider>
    {
        public CatalogController(CatalogProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpGet]
        public async Task<IEnumerable<Drink>> GetDrinks(int pageNumber, int perPage)
        {
            var drinkPageRequest = new DrinkPageRequest
            {
                UserId = "test",
                PageNumber = pageNumber,
                PerPage = perPage
            };
            
            var call = ServiceProvider.Client.getDrinksPage(drinkPageRequest);
            var result = await call.ResponseStream.ToListAsync();
            return result;
        }
    }
}
