using Microsoft.AspNetCore.Mvc;

namespace prodrink.gateway.MvcControllers.Test
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        [HttpGet]
        public string[] Values()
        {
            return new[] {"Test", "test2"};
        }
    }
}