using Newtonsoft.Json;
using System.IO;

namespace FedGraph.Main
{
    public class CVertex
    {
        public int id { get; set; }
        public string info { get; set; }
        public List<int> adj_v { get; set; }
    }
    public class CServer
    {
        public int id { get; set; }
        public string address { get; set; }
    }

    public class CEdge
    {
        public int id { get; set; }
        public int weight { get; set; }
    }
    public class CAdjVertex
    {
        public int id { get; set; }
        public List<CEdge> edges { get; set; }
    }

    public class Config
    {
        public List<CVertex> vertexes { get; set; }
        public List<CServer> servers { get; set; }
        public List<CAdjVertex> adj_list { get; set; }
    }
    public class Parsing
    {
        public static Config parse(string filename="config.json")
        {
            Config config;
            using (StreamReader file = File.OpenText(filename))
            {
                JsonSerializer serializer= new JsonSerializer(); 
                config = (Config) serializer.Deserialize(file, typeof(Config));
            }
            return config;
        }
        public static void serialize(Config config, string filename)
        {
            using (StreamWriter file = File.CreateText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, config);
            }
        }
    }
}
