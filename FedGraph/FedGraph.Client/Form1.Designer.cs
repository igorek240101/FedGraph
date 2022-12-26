namespace FedGraph.Client
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nodesNumLabel = new System.Windows.Forms.Label();
            this.startNodeTextBox = new System.Windows.Forms.TextBox();
            this.endNodeTextBox = new System.Windows.Forms.TextBox();
            this.findPathBtn = new System.Windows.Forms.Button();
            this.pathLengthLabel = new System.Windows.Forms.Label();
            this.shortestPathLabel = new System.Windows.Forms.Label();
            this.saveResultBtn = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.графToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startNodeInvalidLabel = new System.Windows.Forms.Label();
            this.endNodeInvalidLabel = new System.Windows.Forms.Label();
            this.debug = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Количество вершин в графе:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Начальная вершина:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Конечная вершина:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 290);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Результат:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(50, 270);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 20);
            this.label5.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(50, 336);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Длина пути:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(50, 380);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "Кратчайший путь:";
            // 
            // nodesNumLabel
            // 
            this.nodesNumLabel.AutoSize = true;
            this.nodesNumLabel.Location = new System.Drawing.Point(265, 51);
            this.nodesNumLabel.Name = "nodesNumLabel";
            this.nodesNumLabel.Size = new System.Drawing.Size(20, 20);
            this.nodesNumLabel.TabIndex = 7;
            this.nodesNumLabel.Text = "N";
            // 
            // startNodeTextBox
            // 
            this.startNodeTextBox.Location = new System.Drawing.Point(229, 94);
            this.startNodeTextBox.Name = "startNodeTextBox";
            this.startNodeTextBox.Size = new System.Drawing.Size(56, 27);
            this.startNodeTextBox.TabIndex = 8;
            // 
            // endNodeTextBox
            // 
            this.endNodeTextBox.Location = new System.Drawing.Point(229, 167);
            this.endNodeTextBox.Name = "endNodeTextBox";
            this.endNodeTextBox.Size = new System.Drawing.Size(56, 27);
            this.endNodeTextBox.TabIndex = 9;
            // 
            // findPathBtn
            // 
            this.findPathBtn.Location = new System.Drawing.Point(51, 230);
            this.findPathBtn.Name = "findPathBtn";
            this.findPathBtn.Size = new System.Drawing.Size(235, 40);
            this.findPathBtn.TabIndex = 10;
            this.findPathBtn.Text = "Найти путь";
            this.findPathBtn.UseVisualStyleBackColor = true;
            this.findPathBtn.Click += new System.EventHandler(this.findPathBtn_Click);
            // 
            // pathLengthLabel
            // 
            this.pathLengthLabel.AutoSize = true;
            this.pathLengthLabel.Location = new System.Drawing.Point(147, 336);
            this.pathLengthLabel.Name = "pathLengthLabel";
            this.pathLengthLabel.Size = new System.Drawing.Size(0, 20);
            this.pathLengthLabel.TabIndex = 11;
            // 
            // shortestPathLabel
            // 
            this.shortestPathLabel.AutoSize = true;
            this.shortestPathLabel.Location = new System.Drawing.Point(189, 380);
            this.shortestPathLabel.Name = "shortestPathLabel";
            this.shortestPathLabel.Size = new System.Drawing.Size(0, 20);
            this.shortestPathLabel.TabIndex = 12;
            this.shortestPathLabel.Click += new System.EventHandler(this.label9_Click);
            // 
            // saveResultBtn
            // 
            this.saveResultBtn.Location = new System.Drawing.Point(507, 280);
            this.saveResultBtn.Name = "saveResultBtn";
            this.saveResultBtn.Size = new System.Drawing.Size(235, 40);
            this.saveResultBtn.TabIndex = 13;
            this.saveResultBtn.Text = "Сохранить результат";
            this.saveResultBtn.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.графToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // графToolStripMenuItem
            // 
            this.графToolStripMenuItem.Name = "графToolStripMenuItem";
            this.графToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.графToolStripMenuItem.Text = "Граф";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // startNodeInvalidLabel
            // 
            this.startNodeInvalidLabel.AutoSize = true;
            this.startNodeInvalidLabel.ForeColor = System.Drawing.Color.Red;
            this.startNodeInvalidLabel.Location = new System.Drawing.Point(51, 124);
            this.startNodeInvalidLabel.Name = "startNodeInvalidLabel";
            this.startNodeInvalidLabel.Size = new System.Drawing.Size(0, 20);
            this.startNodeInvalidLabel.TabIndex = 15;
            // 
            // endNodeInvalidLabel
            // 
            this.endNodeInvalidLabel.AutoSize = true;
            this.endNodeInvalidLabel.ForeColor = System.Drawing.Color.Red;
            this.endNodeInvalidLabel.Location = new System.Drawing.Point(51, 197);
            this.endNodeInvalidLabel.Name = "endNodeInvalidLabel";
            this.endNodeInvalidLabel.Size = new System.Drawing.Size(0, 20);
            this.endNodeInvalidLabel.TabIndex = 16;
            // 
            // debug
            // 
            this.debug.AutoSize = true;
            this.debug.Location = new System.Drawing.Point(380, 51);
            this.debug.Name = "debug";
            this.debug.Size = new System.Drawing.Size(209, 20);
            this.debug.TabIndex = 17;
            this.debug.Text = "Количество вершин в графе:";
            this.debug.Click += new System.EventHandler(this.label8_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 491);
            this.Controls.Add(this.debug);
            this.Controls.Add(this.endNodeInvalidLabel);
            this.Controls.Add(this.startNodeInvalidLabel);
            this.Controls.Add(this.saveResultBtn);
            this.Controls.Add(this.shortestPathLabel);
            this.Controls.Add(this.pathLengthLabel);
            this.Controls.Add(this.findPathBtn);
            this.Controls.Add(this.endNodeTextBox);
            this.Controls.Add(this.startNodeTextBox);
            this.Controls.Add(this.nodesNumLabel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label nodesNumLabel;
        private TextBox startNodeTextBox;
        private TextBox endNodeTextBox;
        private Button findPathBtn;
        private Label pathLengthLabel;
        private Label shortestPathLabel;
        private Button saveResultBtn;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem графToolStripMenuItem;
        private ToolStripMenuItem справкаToolStripMenuItem;
        private Label startNodeInvalidLabel;
        private Label endNodeInvalidLabel;
        private Label debug;
    }
}