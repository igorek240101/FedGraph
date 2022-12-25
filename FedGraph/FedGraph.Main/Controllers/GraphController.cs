namespace FedGraph.Main
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class GraphController : ControllerBase
    {
        IHttpClientFactory clientFactory;
        public GraphController(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }
        // Проверяем, есть ли в графе вершина с таким id
        [HttpGet("contains/{id}")]
        public JsonResult GetContainsVertex(int id)
        {
            if (Application.graph.containsVertex(id))
            {
                return new JsonResult(true);
            }
            return new JsonResult(false);
        }
        // Запускаем алгоритм Дейкстры с вершины с таким id
        [HttpGet("search/start/{id}")]
        public void PostStartSearch(int id)
        {
            if (id != null)
            {
                Console.WriteLine(id);
                Application.graph.setStartVertexId(id);
                
                
                var client = clientFactory.CreateClient();
                Application.graph.dijksra(client);
                /*
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    "https://localhost:7052/api/graph/search/end/6");
                var response = client.Send(request);
                */
            }
        }
        // Устанавливаем конечную вершину для алгоритма Дейкстры
        [HttpGet("search/end/{id}")]
        public void PostEndSearch(int id)
        {
            Application.graph.setEndVertexId(id);
            Application.graph.getShortestPath();
        }

    }
}
