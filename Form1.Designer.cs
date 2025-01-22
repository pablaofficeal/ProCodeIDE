namespace WinFormsApp7
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox editorTextBox; // Редактор кода
        private System.Windows.Forms.Button runButton;      // Кнопка запуска
        private System.Windows.Forms.TextBox outputTextBox; // Консоль для вывода
        private System.Windows.Forms.MenuStrip menuStrip1; // Меню
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem цветToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem чорнийToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem белыйToolStripMenuItem;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            editorTextBox = new TextBox();
            runButton = new Button();
            outputTextBox = new TextBox();
            menuStrip1 = new MenuStrip();
            настройкиToolStripMenuItem = new ToolStripMenuItem();
            цветToolStripMenuItem = new ToolStripMenuItem();
            чорнийToolStripMenuItem = new ToolStripMenuItem();
            белыйToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // editorTextBox
            // 
            editorTextBox.AcceptsReturn = true;
            editorTextBox.AcceptsTab = true;
            editorTextBox.AllowDrop = true;
            editorTextBox.BackColor = Color.FromArgb(174, 253, 255);
            editorTextBox.BorderStyle = BorderStyle.FixedSingle;
            editorTextBox.Dock = DockStyle.Fill;
            editorTextBox.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            editorTextBox.Location = new Point(0, 49);
            editorTextBox.Multiline = true;
            editorTextBox.Name = "editorTextBox";
            editorTextBox.ScrollBars = ScrollBars.Both;
            editorTextBox.Size = new Size(961, 445);
            editorTextBox.TabIndex = 2;
            // 
            // runButton
            // 
            runButton.AutoSize = true;
            runButton.BackColor = SystemColors.ButtonHighlight;
            runButton.Dock = DockStyle.Top;
            runButton.ForeColor = SystemColors.ActiveCaptionText;
            runButton.Image = (Image)resources.GetObject("runButton.Image");
            runButton.Location = new Point(0, 24);
            runButton.Name = "runButton";
            runButton.Size = new Size(961, 25);
            runButton.TabIndex = 1;
            runButton.UseVisualStyleBackColor = false;
            runButton.Click += RunButton_Click;
            // 
            // outputTextBox
            // 
            outputTextBox.BackColor = SystemColors.ActiveBorder;
            outputTextBox.Dock = DockStyle.Bottom;
            outputTextBox.Location = new Point(0, 372);
            outputTextBox.Multiline = true;
            outputTextBox.Name = "outputTextBox";
            outputTextBox.ReadOnly = true;
            outputTextBox.ScrollBars = ScrollBars.Vertical;
            outputTextBox.Size = new Size(961, 122);
            outputTextBox.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { настройкиToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(961, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            menuStrip1.ItemClicked += menuStrip1_ItemClicked_1;
            // 
            // настройкиToolStripMenuItem
            // 
            настройкиToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { цветToolStripMenuItem });
            настройкиToolStripMenuItem.Image = (Image)resources.GetObject("настройкиToolStripMenuItem.Image");
            настройкиToolStripMenuItem.MergeAction = MergeAction.MatchOnly;
            настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            настройкиToolStripMenuItem.Size = new Size(95, 20);
            настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // цветToolStripMenuItem
            // 
            цветToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { чорнийToolStripMenuItem, белыйToolStripMenuItem });
            цветToolStripMenuItem.Name = "цветToolStripMenuItem";
            цветToolStripMenuItem.Size = new Size(100, 22);
            цветToolStripMenuItem.Text = "Цвет";
            // 
            // чорнийToolStripMenuItem
            // 
            чорнийToolStripMenuItem.Name = "чорнийToolStripMenuItem";
            чорнийToolStripMenuItem.Size = new Size(118, 22);
            чорнийToolStripMenuItem.Text = "Черный";
            чорнийToolStripMenuItem.Click += ChangeToBlackTheme;
            // 
            // белыйToolStripMenuItem
            // 
            белыйToolStripMenuItem.Name = "белыйToolStripMenuItem";
            белыйToolStripMenuItem.Size = new Size(118, 22);
            белыйToolStripMenuItem.Text = "Белый";
            белыйToolStripMenuItem.Click += ChangeToWhiteTheme;
            // 
            // Form1
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = SystemColors.ActiveBorder;
            ClientSize = new Size(961, 494);
            Controls.Add(outputTextBox);
            Controls.Add(editorTextBox);
            Controls.Add(runButton);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "ProCode IDE";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        // Обработчик кнопки Run
        private void RunButton_Click(object sender, EventArgs e)
        {
            string code = editorTextBox.Text;

            try
            {
                outputTextBox.Text = ExecutePythonCode(code);
            }
            catch (Exception ex)
            {
                outputTextBox.Text = $"Error: {ex.Message}";
            }
        }

        // Метод для выполнения Python-кода
        private string ExecutePythonCode(string code)
        {
            string tempFile = System.IO.Path.GetTempFileName() + ".py";
            System.IO.File.WriteAllText(tempFile, code);

            var process = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = @"C:\Python39\python.exe", // Путь к Python
                    Arguments = $"\"{tempFile}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (!string.IsNullOrEmpty(error))
            {
                return $"Error: {error}";
            }
            return output;
        }

        // Логика изменения тем
        private void ChangeToBlackTheme(object sender, EventArgs e)
        {
            BackColor = Color.Black;
            editorTextBox.BackColor = Color.Black;
            editorTextBox.ForeColor = Color.White;
            outputTextBox.BackColor = Color.Black;
            outputTextBox.ForeColor = Color.White;
        }

        private void ChangeToWhiteTheme(object sender, EventArgs e)
        {
            BackColor = Color.White;
            editorTextBox.BackColor = Color.White;
            editorTextBox.ForeColor = Color.Black;
            outputTextBox.BackColor = Color.White;
            outputTextBox.ForeColor = Color.Black;
        }


    }
}
