using ASP.Net.Core.Unity.Example.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace ASP.Net.Core.Unity.Example.Controllers
{
    [Route("")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly SomeClass _someClass;
        private readonly HttpClient _httpClient;
        private readonly ILogger<ValuesController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IService _service;

        public ValuesController(SomeClass someClass, HttpClient httpClient, ILogger<ValuesController> logger, IConfiguration configuration, IService service)
        {
            _someClass = someClass;
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                LoggerName = _logger.GetType().FullName,
                SomeClassValue = _someClass.Value,
                SecretPasswordFromConfiguration = _configuration["TestConfiguration:SecretPassword"]
            });
        }
    }
}
