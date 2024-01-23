using Microsoft.AspNetCore.Mvc;
using ActionFilterApi.Filters;
using ActionFilterApi.Classes;

namespace ActionFilterApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly ILogger<DemoController> _logger;

        public DemoController(ILogger<DemoController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [HttpGet(Name = "Run")]
        [ServiceFilter(typeof(ApiLogActionFilter))]
        public async Task<IActionResult> Post([FromBody] DemoRequestData demoRequestData)
        {
            //do your async stuff here
            //...
            //...

            var demoResponseData = new DemoResponseData()
            {
                Id = "some id for your response db item???",
                Message = "Sample response data",
                RequestId = demoRequestData.Id
            };

            //return success for demonstration purposes
            return Ok(demoResponseData);
        }
    }
}
