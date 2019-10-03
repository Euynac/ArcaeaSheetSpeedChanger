namespace ArcaeaSpeedChanger
{
    partial class SheetChanger
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SheetChanger));
            this.openFileButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sheetUrlLabel = new System.Windows.Forms.Label();
            this.generateButton = new System.Windows.Forms.Button();
            this.SpeedComboBox = new MetroFramework.Controls.MetroComboBox();
            this.closeBtn = new MetroFramework.Controls.MetroButton();
            this.soundCheckBox = new System.Windows.Forms.CheckBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.barLineCheckBox = new System.Windows.Forms.CheckBox();
            this.rapidPackCheckBox = new System.Windows.Forms.CheckBox();
            this.combineJsonBtn = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // openFileButton
            // 
            this.openFileButton.BackColor = System.Drawing.SystemColors.Control;
            this.openFileButton.FlatAppearance.BorderSize = 0;
            this.openFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openFileButton.Location = new System.Drawing.Point(65, 279);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(262, 99);
            this.openFileButton.TabIndex = 1;
            this.openFileButton.Text = "Open File";
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.OpenFileButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(92, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "倍数";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(92, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "谱面路径";
            // 
            // sheetUrlLabel
            // 
            this.sheetUrlLabel.AutoSize = true;
            this.sheetUrlLabel.BackColor = System.Drawing.Color.Transparent;
            this.sheetUrlLabel.Location = new System.Drawing.Point(93, 210);
            this.sheetUrlLabel.Name = "sheetUrlLabel";
            this.sheetUrlLabel.Size = new System.Drawing.Size(89, 18);
            this.sheetUrlLabel.TabIndex = 4;
            this.sheetUrlLabel.Text = "C:\\arcaea";
            // 
            // generateButton
            // 
            this.generateButton.BackColor = System.Drawing.Color.Transparent;
            this.generateButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.generateButton.FlatAppearance.BorderSize = 0;
            this.generateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.generateButton.Location = new System.Drawing.Point(65, 409);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(262, 99);
            this.generateButton.TabIndex = 5;
            this.generateButton.Text = "Generate";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // SpeedComboBox
            // 
            this.SpeedComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.SpeedComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SpeedComboBox.FormattingEnabled = true;
            this.SpeedComboBox.ItemHeight = 23;
            this.SpeedComboBox.Location = new System.Drawing.Point(220, 62);
            this.SpeedComboBox.Name = "SpeedComboBox";
            this.SpeedComboBox.Size = new System.Drawing.Size(108, 29);
            this.SpeedComboBox.TabIndex = 13;
            this.SpeedComboBox.UseSelectable = true;
            this.SpeedComboBox.SelectedIndexChanged += new System.EventHandler(this.SpeedComboBox_SelectedIndexChanged);
            // 
            // closeBtn
            // 
            this.closeBtn.BackColor = System.Drawing.Color.Transparent;
            this.closeBtn.Location = new System.Drawing.Point(796, 22);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(30, 31);
            this.closeBtn.TabIndex = 14;
            this.closeBtn.Text = "X";
            this.closeBtn.UseSelectable = true;
            this.closeBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // soundCheckBox
            // 
            this.soundCheckBox.AutoSize = true;
            this.soundCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.soundCheckBox.Location = new System.Drawing.Point(51, 573);
            this.soundCheckBox.Name = "soundCheckBox";
            this.soundCheckBox.Size = new System.Drawing.Size(106, 22);
            this.soundCheckBox.TabIndex = 16;
            this.soundCheckBox.Text = "音频变速";
            this.soundCheckBox.UseVisualStyleBackColor = false;
            this.soundCheckBox.CheckedChanged += new System.EventHandler(this.SoundCheckBox_CheckedChanged);
            // 
            // progressBar
            // 
            this.progressBar.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.progressBar.Location = new System.Drawing.Point(38, 603);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(797, 25);
            this.progressBar.TabIndex = 18;
            // 
            // barLineCheckBox
            // 
            this.barLineCheckBox.AutoSize = true;
            this.barLineCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.barLineCheckBox.Location = new System.Drawing.Point(51, 545);
            this.barLineCheckBox.Name = "barLineCheckBox";
            this.barLineCheckBox.Size = new System.Drawing.Size(241, 22);
            this.barLineCheckBox.TabIndex = 19;
            this.barLineCheckBox.Text = "小节线对齐（BPM会改变）";
            this.barLineCheckBox.UseVisualStyleBackColor = false;
            // 
            // rapidPackCheckBox
            // 
            this.rapidPackCheckBox.AutoSize = true;
            this.rapidPackCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.rapidPackCheckBox.Location = new System.Drawing.Point(51, 517);
            this.rapidPackCheckBox.Name = "rapidPackCheckBox";
            this.rapidPackCheckBox.Size = new System.Drawing.Size(106, 22);
            this.rapidPackCheckBox.TabIndex = 20;
            this.rapidPackCheckBox.Text = "快速打包";
            this.rapidPackCheckBox.UseVisualStyleBackColor = false;
            this.rapidPackCheckBox.CheckedChanged += new System.EventHandler(this.RapidPackCheckBox_CheckedChanged);
            // 
            // combineJsonBtn
            // 
            this.combineJsonBtn.BackColor = System.Drawing.Color.Transparent;
            this.combineJsonBtn.Location = new System.Drawing.Point(163, 517);
            this.combineJsonBtn.Name = "combineJsonBtn";
            this.combineJsonBtn.Size = new System.Drawing.Size(116, 22);
            this.combineJsonBtn.TabIndex = 21;
            this.combineJsonBtn.Text = "合并json";
            this.combineJsonBtn.UseSelectable = true;
            this.combineJsonBtn.Click += new System.EventHandler(this.CombineJsonBtn_Click);
            // 
            // SheetChanger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::ArcaeaSpeedChanger.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(942, 629);
            this.ControlBox = false;
            this.Controls.Add(this.combineJsonBtn);
            this.Controls.Add(this.rapidPackCheckBox);
            this.Controls.Add(this.barLineCheckBox);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.soundCheckBox);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.SpeedComboBox);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.sheetUrlLabel);
            this.Controls.Add(this.openFileButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SheetChanger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Arcaea谱面变速器";
            this.Load += new System.EventHandler(this.SheetChanger_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SheetChanger_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label sheetUrlLabel;
        private System.Windows.Forms.Button generateButton;
        private MetroFramework.Controls.MetroComboBox SpeedComboBox;
        private MetroFramework.Controls.MetroButton closeBtn;
        private System.Windows.Forms.CheckBox soundCheckBox;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.CheckBox barLineCheckBox;
        private System.Windows.Forms.CheckBox rapidPackCheckBox;
        private MetroFramework.Controls.MetroButton combineJsonBtn;
    }
}

