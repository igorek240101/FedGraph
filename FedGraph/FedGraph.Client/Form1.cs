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

        private void findPathBtn_Click(object sender, EventArgs e)
        {
            string startNodeValue = this.startNodeTextBox.Text;
            string endNodeValue = this.endNodeTextBox.Text;
            if (valuesIsValid(startNodeValue, endNodeValue)) {
                int startVertexId = int.Parse(startNodeValue);
                int endVertexId = int.Parse(endNodeValue);
                try
                {
                    // Запускаем алгоритм поиска кратчайшего пути
                    List<Path> shortestPath = Service.dijkstra(startVertexId, endVertexId);
                    if (shortestPath != null)
                    {
                        pathLengthLabel.Text = shortestPath[0].min_length.ToString();
                        string pathStr = "";
                        for (int i = shortestPath.Count() - 1; i >= 0; i--)
                        {
                            pathStr = pathStr + shortestPath[i].vertex.id.ToString() + " ";
                        }
                        shortestPathLabel.Text = pathStr;
                        debug.Text = "";
                    }
                    else
                    {
                        debug.Text = "Нет кратчайшего пути";
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

        public void CreateInfoForm()
        {
            Form form1 = new Form();
            Button button1 = new Button();
            Label label = new Label();
                        form1.Width = 600;
            form1.Height = 300;
            label.Width = 590;
            label.Height = 150;
            button1.Width = 100;
            button1.Height = 30;

            label.Location = new Point(10, 10);
            label.Text = "Для нахождение кротчайшего пути в графе между вершинами с помощью алгоритма Дейкстры " +
                "заполните поля \"Начальная вершина\" и \"Конечная вершина\", и нажмите на кнопку \"Найти путь\"." +
                "\nПрограмма отобразит длину найденного минимального пути и сам путь из вершин." +
                "\nДля сохранения результата в текстовый файл нажмите кнопку \"Сохранить результат\".";
            button1.Text = "Закрыть";
            button1.Location = new Point(10, 180);
            form1.Text = "Справка";
            form1.HelpButton = true;
            
            form1.FormBorderStyle = FormBorderStyle.FixedDialog;
            form1.MaximizeBox = false;
            form1.MinimizeBox = false;
            form1.CancelButton = button1;
            form1.StartPosition = FormStartPosition.CenterScreen;

            form1.Controls.Add(label);
            form1.Controls.Add(button1);

            form1.ShowDialog();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateInfoForm();
        }
        
        private void saveResultBtn_Click(object sender, EventArgs e)
        {
            string startId = startNodeTextBox.Text;
            string endId = endNodeTextBox.Text;
            string length = pathLengthLabel.Text;
            string path = shortestPathLabel.Text;

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = "Path";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*.txt";
            var result = dialog.ShowDialog();

            // Process save file dialog box results
            if (result == DialogResult.OK)
            {
                // Save document
                string filename = dialog.FileName;
                Service.savePath(startId, endId, length, path, filename);
            }
            
        }
    }
}