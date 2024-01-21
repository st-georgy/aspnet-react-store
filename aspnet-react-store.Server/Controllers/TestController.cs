using Microsoft.AspNetCore.Mvc;

namespace aspnet_react_store.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase {

        [HttpGet]
        public string Get() => "test";
    }
}
