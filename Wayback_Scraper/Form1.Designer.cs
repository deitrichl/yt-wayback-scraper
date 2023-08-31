namespace Wayback_Scraper
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            pictureBox1 = new PictureBox();
            textBox3 = new TextBox();
            button2 = new Button();
            button3 = new Button();
            listBox1 = new ListBox();
            button4 = new Button();
            button5 = new Button();
            checkBox1 = new CheckBox();
            textBox1 = new TextBox();
            panel1 = new Panel();
            button9 = new Button();
            panel2 = new Panel();
            button8 = new Button();
            button6 = new Button();
            label1 = new Label();
            label2 = new Label();
            button7 = new Button();
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            comboBox3 = new ComboBox();
            textBox4 = new TextBox();
            comboBox4 = new ComboBox();
            openFileDialog1 = new OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.SteelBlue;
            button1.Cursor = Cursors.Hand;
            button1.FlatAppearance.BorderColor = Color.Maroon;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button1.ForeColor = SystemColors.ControlLightLight;
            button1.Location = new Point(807, 134);
            button1.Margin = new Padding(0);
            button1.Name = "button1";
            button1.Size = new Size(99, 44);
            button1.TabIndex = 0;
            button1.Text = "Submit";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(39, 35);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(303, 83);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // textBox3
            // 
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Cursor = Cursors.IBeam;
            textBox3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox3.Location = new Point(3, 12);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(753, 22);
            textBox3.TabIndex = 4;
            // 
            // button2
            // 
            button2.BackColor = Color.Silver;
            button2.Cursor = Cursors.Hand;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button2.Location = new Point(807, 525);
            button2.Name = "button2";
            button2.Size = new Size(99, 44);
            button2.TabIndex = 5;
            button2.Text = "Browse...";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.White;
            button3.FlatAppearance.BorderColor = Color.Silver;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Image = (Image)resources.GetObject("button3.Image");
            button3.ImageAlign = ContentAlignment.MiddleLeft;
            button3.Location = new Point(609, 9);
            button3.Name = "button3";
            button3.Size = new Size(65, 23);
            button3.TabIndex = 7;
            button3.Text = "Paste";
            button3.TextAlign = ContentAlignment.MiddleRight;
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // listBox1
            // 
            listBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 21;
            listBox1.Location = new Point(39, 232);
            listBox1.Name = "listBox1";
            listBox1.SelectionMode = SelectionMode.MultiExtended;
            listBox1.Size = new Size(867, 151);
            listBox1.TabIndex = 8;
            // 
            // button4
            // 
            button4.BackColor = Color.SeaGreen;
            button4.Cursor = Cursors.Hand;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button4.ForeColor = SystemColors.Control;
            button4.Location = new Point(39, 432);
            button4.Name = "button4";
            button4.Size = new Size(421, 31);
            button4.TabIndex = 9;
            button4.Text = "Download Selected";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.BackColor = Color.SeaGreen;
            button5.Cursor = Cursors.Hand;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button5.ForeColor = SystemColors.Control;
            button5.Location = new Point(485, 432);
            button5.Name = "button5";
            button5.Size = new Size(421, 31);
            button5.TabIndex = 10;
            button5.Text = "Download All";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(249, 189);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(398, 19);
            checkBox1.TabIndex = 11;
            checkBox1.Text = "Skip selection and automatically download all available deleted videos.";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.AllowDrop = true;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Cursor = Cursors.IBeam;
            textBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.ForeColor = SystemColors.WindowText;
            textBox1.Location = new Point(10, 10);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Input the url(s)/ID(s) of deleted Youtube videos or a playlist containing deleted videos.";
            textBox1.Size = new Size(593, 22);
            textBox1.TabIndex = 1;
            textBox1.TextChanged += textBox1_TextChanged;
            textBox1.KeyDown += textBox1_KeyDown;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button9);
            panel1.Controls.Add(textBox1);
            panel1.Location = new Point(39, 134);
            panel1.Name = "panel1";
            panel1.Size = new Size(776, 44);
            panel1.TabIndex = 13;
            // 
            // button9
            // 
            button9.BackColor = Color.White;
            button9.FlatAppearance.BorderColor = Color.Silver;
            button9.FlatStyle = FlatStyle.Flat;
            button9.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            button9.ForeColor = SystemColors.ActiveCaptionText;
            button9.Image = (Image)resources.GetObject("button9.Image");
            button9.ImageAlign = ContentAlignment.MiddleLeft;
            button9.Location = new Point(680, 9);
            button9.Name = "button9";
            button9.Size = new Size(73, 23);
            button9.TabIndex = 25;
            button9.Text = "Upload";
            button9.TextAlign = ContentAlignment.MiddleRight;
            button9.UseVisualStyleBackColor = false;
            button9.Click += button9_Click;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.Control;
            panel2.Controls.Add(button8);
            panel2.Controls.Add(textBox3);
            panel2.Location = new Point(39, 525);
            panel2.Name = "panel2";
            panel2.Size = new Size(776, 44);
            panel2.TabIndex = 14;
            // 
            // button8
            // 
            button8.FlatAppearance.BorderColor = Color.DimGray;
            button8.FlatStyle = FlatStyle.Flat;
            button8.Image = (Image)resources.GetObject("button8.Image");
            button8.ImageAlign = ContentAlignment.MiddleLeft;
            button8.Location = new Point(688, 12);
            button8.Name = "button8";
            button8.Size = new Size(65, 23);
            button8.TabIndex = 5;
            button8.Text = "Open";
            button8.TextAlign = ContentAlignment.MiddleRight;
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // button6
            // 
            button6.BackColor = Color.Transparent;
            button6.Cursor = Cursors.Hand;
            button6.FlatAppearance.BorderSize = 0;
            button6.FlatStyle = FlatStyle.Flat;
            button6.Font = new Font("HoloLens MDL2 Assets", 18F, FontStyle.Bold, GraphicsUnit.Point);
            button6.ForeColor = Color.DimGray;
            button6.Location = new Point(902, 12);
            button6.Name = "button6";
            button6.Size = new Size(40, 40);
            button6.TabIndex = 15;
            button6.Text = "X";
            button6.TextAlign = ContentAlignment.TopCenter;
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // label1
            // 
            label1.BorderStyle = BorderStyle.Fixed3D;
            label1.Location = new Point(10, 475);
            label1.Name = "label1";
            label1.Size = new Size(942, 2);
            label1.TabIndex = 16;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(39, 501);
            label2.Name = "label2";
            label2.Size = new Size(96, 21);
            label2.TabIndex = 17;
            label2.Text = "Output Path:";
            // 
            // button7
            // 
            button7.FlatAppearance.BorderColor = Color.LightSkyBlue;
            button7.FlatStyle = FlatStyle.Flat;
            button7.Image = (Image)resources.GetObject("button7.Image");
            button7.Location = new Point(856, 12);
            button7.Name = "button7";
            button7.Size = new Size(40, 40);
            button7.TabIndex = 18;
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Resolution...", "144p", "240p", "360p", "480p", "720p", "1080p", "1440p", "2160p" });
            comboBox1.Location = new Point(785, 389);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 29);
            comboBox1.TabIndex = 19;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // comboBox2
            // 
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Filetype...", "3g2 ", "3gp ", "avi ", "flv ", "mkv ", "mov ", "mp4 ", "mpeg ", "ogv ", "webm ", "wmv" });
            comboBox2.Location = new Point(657, 389);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(90, 29);
            comboBox2.TabIndex = 21;
            // 
            // comboBox3
            // 
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            comboBox3.FormattingEnabled = true;
            comboBox3.Items.AddRange(new object[] { "Video", "Audio" });
            comboBox3.Location = new Point(564, 389);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(87, 29);
            comboBox3.TabIndex = 22;
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            // 
            // textBox4
            // 
            textBox4.BackColor = Color.AliceBlue;
            textBox4.BorderStyle = BorderStyle.None;
            textBox4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox4.Location = new Point(480, 392);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(78, 22);
            textBox4.TabIndex = 23;
            textBox4.Text = "Convert To:";
            // 
            // comboBox4
            // 
            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            comboBox4.FormattingEnabled = true;
            comboBox4.Items.AddRange(new object[] { "Filetype...", "aac ", "aiff ", "flac ", "m4a ", "m4r ", "mmf ", "mp3 ", "ogg ", "opus ", "wav ", "wma" });
            comboBox4.Location = new Point(657, 389);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(90, 29);
            comboBox4.TabIndex = 24;
            // 
            // openFileDialog1
            // 
            openFileDialog1.DefaultExt = "\"txt\"";
            openFileDialog1.FileName = "openFileDialog1.FileName";
            openFileDialog1.Filter = "\"txt files (*.txt) | *.txt |All files (*.*) | * . *\"";
            openFileDialog1.InitialDirectory = "C:\\Documents";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "\"Select a Text File\"";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(963, 594);
            Controls.Add(comboBox4);
            Controls.Add(textBox4);
            Controls.Add(comboBox3);
            Controls.Add(comboBox2);
            Controls.Add(comboBox1);
            Controls.Add(button7);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button6);
            Controls.Add(checkBox1);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(listBox1);
            Controls.Add(button2);
            Controls.Add(pictureBox1);
            Controls.Add(button1);
            Controls.Add(panel1);
            Controls.Add(panel2);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Youtube Wayback Scraper";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private PictureBox pictureBox1;
        private TextBox textBox3;
        private Button button2;
        private Button button3;
        private ListBox listBox1;
        private Button button4;
        private Button button5;
        private CheckBox checkBox1;
        private TextBox textBox1;
        private Panel panel1;
        private Panel panel2;
        private Button button6;
        private Label label1;
        private Label label2;
        private Button button7;
        private Button button8;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private ComboBox comboBox3;
        private TextBox textBox4;
        private ComboBox comboBox4;
        private Button button9;
        private OpenFileDialog openFileDialog1;
    }
}