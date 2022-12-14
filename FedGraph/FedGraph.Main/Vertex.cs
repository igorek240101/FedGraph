using System.Collections;

namespace FedGraph.Main
{
    public class Vertex
    {
        public int id;
        public string info;
        private List<Vertex> adj_vertices;

        public Vertex(int id, string info)
        {
            this.id = id;
            this.info = info;
            adj_vertices = new List<Vertex>();
        }
        public Vertex getAdjVertex(int id)
        {
            return adj_vertices[id];
        }
        public void addAdjVertex(Vertex vertex) 
        { 
            this.adj_vertices.Add(vertex);
        }
    }
}
