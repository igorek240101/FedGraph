namespace FedGraph.Main
{
    public class Path
    {
        public Path? prev;
        public Vertex vertex;
        private int min_length;

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
