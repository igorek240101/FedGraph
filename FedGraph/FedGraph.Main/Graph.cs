using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;

namespace FedGraph.Main
{
    public class Graph
    {
        private int vertexesNum;
        private int[,] matrix; // Матрица смежности
        private List<int> visited; // Посещённые вершины
        private Dictionary<int, Path> pathes; // Словарь key: id, value: Path - путь до вершины
        private List<Vertex> adj_vertexes; // граничащие вершины
        private List<Vertex> vertexes;

        private List<CServer> servers; // Список из сереверов

        private int startVertexId;
        private int endVertexId;

        private int[] vertexesIds; // проиндексированные айдишники вершин

        public Graph(Config config)
        {
            this.vertexesNum = config.vertexes.Count();
            this.matrix = new int[vertexesNum, vertexesNum];
            this.visited = new List<int>();
            this.pathes = new Dictionary<int, Path>();
            this.adj_vertexes = new List<Vertex>();
            this.vertexes = config.vertexes;
            this.startVertexId = -1;
            this.endVertexId = -1;
            this.vertexesIds = new int[vertexesNum];
            this.servers = config.servers;
            Console.WriteLine("vertexesNum: " + vertexesNum);

            for (int i = 0; i < vertexesNum; i++)
            {
                if (config.vertexes[i].adj_vertices.Count() > 0) 
                {
                    adj_vertexes.Add(config.vertexes[i]);
                }
                vertexesIds[i] = config.vertexes[i].id;
            }
            fillMatrix(config);
        }

        public void setStartVertexId(int id)
        {
            startVertexId = id;
        }

        public void setEndVertexId(int id)
        {
            endVertexId = id;
        }

        public bool containsVertex(int id)
        {
            if (vertexesIds.Contains(id)) return true;
            return false;
        }
        private Vertex? getVertexWithId(int id)
        {
            Vertex vertex = null;
            foreach (Vertex v in vertexes)
            {
                if (v.id == id)
                        { vertex = v; }
            }
            return vertex;

        }
        // Заполняет матрицу смежности из конфига
        private void fillMatrix(Config config)
        {
            for (int i = 0; i < vertexesNum; i++)
            {
                for (int j = 0; j < vertexesNum; j++)
                {
                    if (i != j)
                        matrix[i, j] = -1;
                    else
                        matrix[i,j] = 0;
                }
            }
            for (int i = 0; i < vertexesNum; i++)
            {
                for (int j = 0; j < config.adj_list[i].edges.Count(); j++)
                {
                    int v_id = getVertexNum(config.adj_list[i].edges[j].id);
                    matrix[i, v_id] = config.adj_list[i].edges[j].weight;
                }
            }
        }

        private int getVertexIdWithMinLength()
        {
            int minLength = int.MaxValue;
            int id = -1;
            foreach (KeyValuePair<int, Path> p in pathes)
            {   
                if(p.Value.getMinLength() < minLength && !visited.Contains(p.Key))
                {
                    minLength = p.Value.getMinLength();
                    id = p.Key;
                }
            }
            return id; 
        }

        private int getVertexNum(int id)
        {
            int mId;
            for (mId = 0; mId < vertexesNum; mId++)
                if (vertexesIds[mId] == id)
                    break;
            return mId;
        }

        public async void dijksra(HttpClient client, Path recievedPath=null)
        {
            if (startVertexId != -1 && containsVertex(startVertexId))
            {
                Console.WriteLine($"Start {startVertexId}");
                int startId = startVertexId;
                Path startPath;
                if (recievedPath != null)
                    startPath = recievedPath;
                else
                    startPath = new Path(getVertexWithId(startId), 0, null);
                if (pathes.ContainsKey(startId))
                {
                    if (pathes[startId].getMinLength() > recievedPath.getMinLength())
                    {
                        pathes[startId] = recievedPath;
                    }
                }
                else
                {
                    pathes.Add(startId, startPath);
                }
                Console.WriteLine(startPath.min_length);
                if (startPath.prev != null)
                    Console.WriteLine($"id: {startPath.vertex.id}, len: {startPath.min_length}, prev: {startPath.prev.vertex.id}");
                else
                    Console.WriteLine($"id: {startPath.vertex.id}, len: {startPath.min_length}, prev: null");
                while (visited.Count() != vertexesNum)
                {
                    // Получаем вершину с минимальным путём до неё из непоесещённых
                    int vertexId = getVertexIdWithMinLength();
                    // Алгоритм завершается, если больше нет путей
                    if (vertexId == -1) { break; }
                    visited.Add(vertexId);

                    // Ищем порядковый номер айдишника вершины
                    int mId = getVertexNum(vertexId);
                    int weight;
                    for (int i = 0; i < vertexesNum; i++)
                    {
                        if (i != mId) 
                        {
                            weight = matrix[mId, i];
                            if (weight > -1)
                            {
                                // реальный id - id из конфига
                                int vertexRealId = vertexesIds[i];
                                Path prevVertexPath = pathes[vertexId];
                                int prevVertexLength = prevVertexPath.getMinLength();
                                if (!pathes.ContainsKey(vertexRealId))
                                {
                                    Path path = new Path(vertexes[i], weight + prevVertexLength, prevVertexPath);
                                    pathes.Add(vertexRealId, path);
                                    Console.WriteLine($"id: {path.vertex.id}, len: {path.min_length}, prev: {path.prev.vertex.id}");
                                }
                                else
                                {
                                    Path path = pathes[vertexRealId];
                                    if (path.getMinLength() > weight + prevVertexLength)
                                    {
                                        path.setPrevious(prevVertexPath, weight);
                                    }
                                    if (path.prev != null)
                                        Console.WriteLine($"id: {path.vertex.id}, len: {path.min_length}, prev: {path.prev.vertex.id}");
                                    else
                                        Console.WriteLine($"id: {path.vertex.id}, len: {path.min_length}, prev: null");
                                }
                            }
                        }
                    }
                    // Если вершина граничащая
                    try
                    {
                        if (isAdjVertex(vertexId))
                        {
                            foreach (CServer s in servers)
                            {
                                var request = new HttpRequestMessage(HttpMethod.Get, s.address + $"/api/graph/contains/{vertexId}");
                                var response = await client.SendAsync(request);
                                if (await response.Content.ReadAsStringAsync() == "true")
                                {
                                    // Отправляем на сервер сериализованный Path для граничащей вершины
                                    JsonContent content = JsonContent.Create(pathes[vertexId]);
                                    await client.PostAsync(s.address + "/api/graph/search/dijkstra", content);
                                }
                            }
                        }
                    } catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                    
                }
                Console.WriteLine("End");
            }
        }

        public bool isAdjVertex(int vertexId)
        {
            foreach (Vertex v in adj_vertexes) {
               if (v.id == vertexId) { return true; }
            }
            return false;
        }
        public List<Path> getShortestPath()
        {
            List<Path> restoredPath = new List<Path>();
            if (endVertexId != -1)
            {
                restoredPath = restorePath(pathes[endVertexId]);
                // Выводим на экран
                for (int i = restoredPath.Count() - 1; i >= 0; i--)
                {
                    Console.Write(restoredPath[i].vertex.id + " ");
                }
                Console.Write(": " + restoredPath[0].min_length);
                Console.WriteLine();
                return restoredPath;
            }
            return restoredPath;
        }

        private List<Path> restorePath (Path path)
        {
            List<Path> entirePath = new List<Path>();
            while (path != null)
            {
                entirePath.Add(path);
                path = path.prev;
            }
            return entirePath;
        }

        //debug
#if DEBUG
        public void printMatrix()
        {
            for (int i = 0; i < vertexesNum; i++)
            {
                for (int j = 0; j < vertexesNum; j++)
                {
                    Console.Write(matrix[i,j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
        public void printPathes()
        {
            foreach(KeyValuePair<int, Path> p in pathes)
            {
                Console.WriteLine(p.Key + ": " + p.Value.getMinLength());
            }
        }
#endif
        public int getVertexesNum() 
        {
            return this.vertexesNum;
        }
    }
}
