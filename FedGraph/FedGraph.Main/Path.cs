namespace FedGraph.Main
{
    public class Path
    {
        public Path? prev { get; set; }
        public Vertex vertex { get; set;  }
        public int min_length { get; set; }

        public Path(Vertex vertex, int min_length, Path? prev)
        {
            this.vertex = vertex;
            this.min_length = min_length;
            this.prev = prev;
        }

        public int getMinLength()
        {
            return this.min_length;
        }

        public void setPrevious(Path prev, int edgeWeight)
        {
            this.prev = prev;
            this.min_length = prev.getMinLength() + edgeWeight;
        }
    }
}
