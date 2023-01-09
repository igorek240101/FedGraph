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
            // Парсинг кофига клинта и инициализация объекта для создания http запросов
            config = Parsing.parse();
            httpclient = new HttpClient();
        }
        public static async Task<int> getVertexesNum()
        {
            int vertexesNum = 0;
            int adjVertexesNum = 0;
            foreach (Server s in config.servers)
            {
                // Получаем ответ от сервера в виде массива [количество вершин, количество граничащих вершин] для каждого подграфа
                var request = new HttpRequestMessage(HttpMethod.Get, s.address + $"/api/graph/vertexes");
                var response = httpclient.Send(request);
                var content = await response.Content.ReadAsStringAsync();
                var nums = JsonConvert.DeserializeObject<List<int>>(content);
                vertexesNum += nums[0];
                adjVertexesNum += nums[1];
            }
            // Высчитываем количество вершин в графе
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
            }
            catch (Newtonsoft.Json.JsonReaderException exception)
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
        private static bool getSearchIsDone()
        {
            var task = searchIsDone();
            task.Wait();
            var result = task.Result;
            return result;
        }

        private static bool getServerContainsVertex(Server s, int id)
        {
            var task = serverContainsVertex(s, id);
            task.Wait();
            var result = task.Result;
            return result;
        }
        private static List<Path> getGetShortestPath(Server s, int endId)
        {
            var task = getShortestPath(s, endId);
            task.Wait();
            var result = task.Result;
            return result;
        }
        public static List<Path> dijkstra(int startId, int endId)
        {
            // Отправляет на все серверы запрос, по которому на сервере удаляется вся информация о предыдущей работе алгоритма
            resetServers();
            // Проверяет, работает ли данный алгоритм на каком-нибудь и серверов. Если работает, то false, если везде поиск окончен, то true
            if (getSearchIsDone())
            {
                // На каждый сервер отправляем запрос, проверяющий, содержит ли сервер вершину с указанным id
                foreach (Server s in config.servers)
                {
                    if (getServerContainsVertex(s, startId))
                    {
                        // Если сервер содержит указанную вершину, то запускам алгоритм Дейкстры на нём
                        startSearch(s, startId);
                    }
                }
                
                List<Path> path = null;
                List<Path> shortPath = null;
                // Ожидаем, пока серверы закончат выполенине алгоритма
                while (!getSearchIsDone()) { }
                foreach (Server s in config.servers)
                {
                    // Снова ожидаем, пока серверы закончат выполение алгоритма :)
                    while (!getSearchIsDone()) { }
                    // Если серве содержит конечную вершину
                    if (getServerContainsVertex(s, endId))
                    {
                        // Получаем путь до вершины
                        path = getGetShortestPath(s, endId);
                        // Здесь находим путь с наименьшей длиной, если почему-то от разных серверов для одной вершины пришёл путь с разной длиной
                        if (shortPath != null)
                        {
                            if (path[0].min_length < shortPath[0].min_length)
                            {
                                shortPath = path;
                            }
                        }else
                        {
                            shortPath = path;
                        }
                    }
                }
                return shortPath;
            }
            return null;
        }
        public static void resetServers()
        {
            foreach (Server s in config.servers)
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
