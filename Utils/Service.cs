using Microsoft.Extensions.Configuration;

namespace ASP.Net.Core.Unity.Example.Utils
{
    public class Service : IService
    {
        private readonly IConfiguration _configuration;

        public Service(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void DoSth()
        {

        }
    }
}
