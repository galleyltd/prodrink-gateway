using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace prodrink.gateway.Controllers
{
    [Route("[controller]/[action]")]
    public class TestController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Test()
        {
            return new[] { "test", "privet" };
        }

    }
}