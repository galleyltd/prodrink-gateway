using Microsoft.AspNetCore.Mvc;
using prodrink.gateway.Services.Grpc;

namespace prodrink.gateway.Controllers
{
    [Route("[controller]/[action]")]
    public abstract class BaseGatewayController<T> : Controller where T: BaseGrpcProvider
    {
        protected readonly T ServiceProvider;
        
        protected BaseGatewayController(T serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}