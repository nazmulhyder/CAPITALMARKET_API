using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Service.Interface;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IService _service;
        public TestController(IService service)
        {
            _service = service;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(Test testEntity)
        {
            var data = await _service.Tests.AddUpdate(testEntity,"");
            return getResponse(data);

        }

    }
}
