using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FedGraph.Main.Controllers
{  
    [Route("api/[controller]")]
    [ApiController]
    public class DijkstraController : ControllerBase
    {
        IHttpClientFactory _clientFactory;
       
    }
}
