namespace FedGraph.Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private bool isDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        private bool valuesIsValid(string startNodeValue, string endNodeValue) 
        {
            bool isValid = true;
            this.startNodeInvalidLabel.Text = "";
            this.endNodeInvalidLabel.Text = "";
            if (startNodeValue == "")
            {
                this.startNodeInvalidLabel.Text = "Поле должно содержать значение";
                isValid = false;
            }
            else if (!isDigitsOnly(startNodeValue))
            {
                this.startNodeInvalidLabel.Text = "Введены недопустимые символы";
                isValid = false;
            } else if (!int.TryParse(startNodeValue, out int value))
            {
                this.startNodeInvalidLabel.Text = "Число слишком большое";
                isValid = false;
            }
            if (endNodeValue == "")
            {
                this.endNodeInvalidLabel.Text = "Поле должно содержать значение";
                isValid = false;
            } 
            else if (!isDigitsOnly(endNodeValue))
            {
                this.endNodeInvalidLabel.Text = "Введены недопустимые символы";
                isValid = false;
            } else if (!int.TryParse(endNodeValue, out int value2))
            {
                this.endNodeInvalidLabel.Text = "Число слишком большое";
                isValid = false;
            }
            return isValid;
        }

        private async void findPathBtn_Click(object sender, EventArgs e)
        {
            string startNodeValue = this.startNodeTextBox.Text;
            string endNodeValue = this.endNodeTextBox.Text;
            if (valuesIsValid(startNodeValue, endNodeValue)) {
                int startVertexId = int.Parse(startNodeValue);
                int endVertexId = int.Parse(endNodeValue);
                try
                {
                    // Запускаем алгоритм поиска кратчайшего пути
                    List<Path> shortestPath = await Service.dijkstra(startVertexId, endVertexId);
                    if (shortestPath != null)
                    {
                        pathLengthLabel.Text = shortestPath[0].min_length.ToString();
                        string pathStr = "";
                        for (int i = shortestPath.Count() - 1; i >= 0; i--)
                        {
                            pathStr = pathStr + shortestPath[i].vertex.id.ToString() + " ";
                        }
                        shortestPathLabel.Text = pathStr;
                    }
                }catch (System.Net.Http.HttpRequestException exception)
                {
                    debug.Text = "Ошибка подключения";
                }
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            Service.Initialize();
            try
            {
                nodesNumLabel.Text = Service.getVertexesNum().Result.ToString();
            }
            catch (System.AggregateException exception)
            {
                nodesNumLabel.Text = "N";
                debug.Text = "Ошибка подключения";
                
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void pathLengthLabel_Click(object sender, EventArgs e)
        {

        }
    }
}