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
        public async Task<IEnumerable<Category>> GetCategories()
        {
            var requestParams = new TopLevelCategoriesRequest();
            var call = ServiceProvider.Client.getTopLevelCategories(requestParams);
            var result = await call.ResponseStream.ToListAsync();
            return result;
        }
        
        [HttpGet]
        public async Task<IEnumerable<Drink>> GetDrinks(int categoryId)
        {
            var requestParams = new DrinksFromCategoryRequest
            {
                UserId = "test",
                CategoryId = categoryId
            };
            var call = ServiceProvider.Client.getDrinksFromCategory(requestParams);
            var result = await call.ResponseStream.ToListAsync();
            return result;
        }
        
        [HttpGet]
        public async Task<Drink> GetDrinkById(int drinkId)
        {
            var requestParams = new DrinkRequest
            {
                UserId = "test",
                DrinkId = drinkId
            };
            var result = await ServiceProvider.Client.getDrinkByIdAsync(requestParams);
            return result;
        }
    }
}
