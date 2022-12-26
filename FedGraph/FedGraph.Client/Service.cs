using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FedGraph.Client
{
    public class Service
    {
        private static HttpClient httpclient;
        private static async Task<bool> serverContainsVertex(Server s, int vertexId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, s.address + $"/api/graph/contains/{vertexId}");
            var response = httpclient.Send(request);
            var content = await response.Content.ReadAsStringAsync();
            if (content == "true") { return true; }
            return false;
        }
        public static async void dijkstra(int startId, int endId)
        {
            httpclient = new HttpClient();
            Config config = Parsing.parse();
            foreach (Server s in config.servers)
            {
                // Ищем сервер с содержащий указанную начальную вершину
                if (await serverContainsVertex(s, startId)) 
                {
                    
                }
            }
        }
    }
}
