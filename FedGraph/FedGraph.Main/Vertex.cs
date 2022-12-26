using System.Collections;

namespace FedGraph.Main
{
    public class Vertex
    {
        public int id { get; set; }
        public string info { get; set; }
        public List<int> adj_vertices { get; set; }

        public Vertex(int id, string info)
        {
            this.id = id;
            this.info = info;
            adj_vertices = new List<int>();
        }
        public int getAdjVertex(int id)
        {
            return adj_vertices[id];
        }
        public void addAdjVertex(int vertex) 
        { 
            this.adj_vertices.Add(vertex);
        }
    }
}
