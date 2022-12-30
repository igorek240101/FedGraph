using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FedGraph.Client
{
    public class Service
    {
        private static HttpClient httpclient;
        private static Config config;
        public static void Initialize()
        {
            config = Parsing.parse();
            httpclient = new HttpClient();
        }
        public static async Task<int> getVertexesNum()
        {
            int vertexesNum = 0;
            int adjVertexesNum = 0;
            foreach (Server s in config.servers)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, s.address + $"/api/graph/vertexes");
                var response = httpclient.Send(request);
                var content = await response.Content.ReadAsStringAsync();
                var nums = JsonConvert.DeserializeObject<List<int>>(content);
                vertexesNum += nums[0];
                adjVertexesNum += nums[1];
            }
            return vertexesNum - adjVertexesNum / 2;
        }
        private static async Task<bool> serverContainsVertex(Server s, int vertexId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, s.address + $"/api/graph/contains/{vertexId}");
            var response = httpclient.Send(request);
            var content = await response.Content.ReadAsStringAsync();
            if (content == "true") { return true; }
            return false;
        }
        private static async Task<bool> serverIsSearching(Server s)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, s.address + $"/api/graph/search/progress");
            var response = httpclient.Send(request);
            var content = await response.Content.ReadAsStringAsync();
            if (content == "true") { return true; }
            return false;
        }
        private static void startSearch(Server s, int vertexId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, s.address + $"/api/graph/search/start/{vertexId}");
            var response = httpclient.Send(request);
        }
        private static async Task<List<Path>> getShortestPath(Server s, int vertexId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, s.address + $"/api/graph/search/end/{vertexId}");
            var response = httpclient.Send(request);
            string jsonString = await response.Content.ReadAsStringAsync();
            try
            {
                var path = JsonConvert.DeserializeObject<List<Path>>(jsonString);
                return path;
            } catch(Newtonsoft.Json.JsonReaderException exception)
            {
                return null;
            }
        }
        public static async Task<bool> searchIsDone()
        {
            foreach (Server s in config.servers)
            {
                if (await serverIsSearching(s)) { return false; }
            }
            return true;
        }
        public static async Task<List<Path>> dijkstra(int startId, int endId)
        {
            resetServers();
            if (await searchIsDone())
            {
                foreach (Server s in config.servers)
                {
                    // Ищем сервер с содержащий указанную начальную вершину
                    if (await serverContainsVertex(s, startId))
                    {
                        startSearch(s, startId);
                    }
                }
                List<Path> path = null;
                while (!await searchIsDone()) { }
                foreach (Server s in config.servers)
                {
                    // Ищем сервер с содержащий указанную конечную вершину
                    if (await serverContainsVertex(s, endId))
                    {
                        path = await getShortestPath(s, endId);
                        break;
                    }
                }
                return path;
            }
            return null;
        }
        public static void resetServers()
        {
            foreach(Server s in config.servers)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, s.address + "/api/graph/reset");
                httpclient.Send(request);
            }
        }

        public static void savePath(string startId, string endId, string length, string path, string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename, false))
            {
                string str = $"Начальная вершина: {startId}\n" +
                    $"Конечная вершина: {endId}\n" +
                    $"Длина пути: {length}\n" +
                    $"Кратчайший путь: {path}";
                writer.WriteLine(str);
            }
        }
    }
}
