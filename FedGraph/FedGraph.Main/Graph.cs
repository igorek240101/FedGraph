namespace FedGraph.Main
{
    public class Graph
    {
        private int vertexesNum;
        private int[,] matrix;
        private int[] visited;
        private Path path;

        public Graph(int vertexesNum)
        {
            this.vertexesNum = vertexesNum;
            this.matrix = new int[vertexesNum, vertexesNum];
            this.visited = new int[vertexesNum];
            path = new Path(new Vertex(1, ""), 0, null);
        }
        
        public void fillMatrix(Config config)
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
                    int v_id = config.adj_list[i].edges[j].id - 1;
                    matrix[i,v_id] = config.adj_list[i].edges[j].weight;
                }
            }
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
#endif
        public int getVertexesNum() 
        {
            return this.vertexesNum;
        }
    }
}
