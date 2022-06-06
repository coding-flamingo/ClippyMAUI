using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExistingBlazorSite.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClippyController : ControllerBase
    {

        private readonly string[] _responseArrays;
        public ClippyController ()
        {
            _responseArrays = new string[]
            {
                "Coding Flamingo Is AWESOME!!",
                "I love .NET MAUI!",
                "I am an everything developer now",
                "Clippy should still be a thing!",
                "Hello MAUI Fest!",
            };
        }


        [HttpGet]
        public string Get()
        {
            return _responseArrays[Random.Shared.Next(0, 5)];
        }
    }
}
