namespace CSToolProject
{
	partial class ToolBar
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.PencilToolBtn = new System.Windows.Forms.Button();
            this.ToolBarLabel = new System.Windows.Forms.Label();
            this.EraserToolBtn = new System.Windows.Forms.Button();
            this.ColorPickerBtn = new System.Windows.Forms.Button();
            this.ZoomInBtn = new System.Windows.Forms.Button();
            this.ZoomOutBtn = new System.Windows.Forms.Button();
            this.ToolSize = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.ToolSize)).BeginInit();
            this.SuspendLayout();
            // 
            // PencilToolBtn
            // 
            this.PencilToolBtn.Location = new System.Drawing.Point(3, 19);
            this.PencilToolBtn.Name = "PencilToolBtn";
            this.PencilToolBtn.Size = new System.Drawing.Size(75, 23);
            this.PencilToolBtn.TabIndex = 0;
            this.PencilToolBtn.Text = "Pencil";
            this.PencilToolBtn.UseVisualStyleBackColor = true;
            this.PencilToolBtn.Click += new System.EventHandler(this.PencilToolBtn_Click);
            // 
            // ToolBarLabel
            // 
            this.ToolBarLabel.AutoSize = true;
            this.ToolBarLabel.Location = new System.Drawing.Point(25, 3);
            this.ToolBarLabel.Name = "ToolBarLabel";
            this.ToolBarLabel.Size = new System.Drawing.Size(33, 13);
            this.ToolBarLabel.TabIndex = 1;
            this.ToolBarLabel.Text = "Tools";
            // 
            // EraserToolBtn
            // 
            this.EraserToolBtn.Location = new System.Drawing.Point(3, 48);
            this.EraserToolBtn.Name = "EraserToolBtn";
            this.EraserToolBtn.Size = new System.Drawing.Size(75, 23);
            this.EraserToolBtn.TabIndex = 2;
            this.EraserToolBtn.Text = "Eraser";
            this.EraserToolBtn.UseVisualStyleBackColor = true;
            this.EraserToolBtn.Click += new System.EventHandler(this.EraserToolBtn_Click);
            // 
            // ColorPickerBtn
            // 
            this.ColorPickerBtn.Location = new System.Drawing.Point(3, 77);
            this.ColorPickerBtn.Name = "ColorPickerBtn";
            this.ColorPickerBtn.Size = new System.Drawing.Size(75, 23);
            this.ColorPickerBtn.TabIndex = 3;
            this.ColorPickerBtn.Text = "Color";
            this.ColorPickerBtn.UseVisualStyleBackColor = true;
            this.ColorPickerBtn.Click += new System.EventHandler(this.ColorPickerBtn_Click);
            // 
            // ZoomInBtn
            // 
            this.ZoomInBtn.Location = new System.Drawing.Point(3, 106);
            this.ZoomInBtn.Name = "ZoomInBtn";
            this.ZoomInBtn.Size = new System.Drawing.Size(75, 23);
            this.ZoomInBtn.TabIndex = 4;
            this.ZoomInBtn.Text = "Zoom In";
            this.ZoomInBtn.UseVisualStyleBackColor = true;
            this.ZoomInBtn.Click += new System.EventHandler(this.ZoomInBtn_Click);
            // 
            // ZoomOutBtn
            // 
            this.ZoomOutBtn.Location = new System.Drawing.Point(3, 135);
            this.ZoomOutBtn.Name = "ZoomOutBtn";
            this.ZoomOutBtn.Size = new System.Drawing.Size(75, 23);
            this.ZoomOutBtn.TabIndex = 5;
            this.ZoomOutBtn.Text = "Zoom Out";
            this.ZoomOutBtn.UseVisualStyleBackColor = true;
            this.ZoomOutBtn.Click += new System.EventHandler(this.ZoomOutBtn_Click);
            // 
            // ToolSize
            // 
            this.ToolSize.Location = new System.Drawing.Point(4, 165);
            this.ToolSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ToolSize.Name = "ToolSize";
            this.ToolSize.Size = new System.Drawing.Size(74, 20);
            this.ToolSize.TabIndex = 6;
            this.ToolSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ToolSize.ValueChanged += new System.EventHandler(this.ToolSize_ValueChanged);
            // 
            // ToolBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ToolSize);
            this.Controls.Add(this.ZoomOutBtn);
            this.Controls.Add(this.ZoomInBtn);
            this.Controls.Add(this.ColorPickerBtn);
            this.Controls.Add(this.EraserToolBtn);
            this.Controls.Add(this.ToolBarLabel);
            this.Controls.Add(this.PencilToolBtn);
            this.Name = "ToolBar";
            this.Size = new System.Drawing.Size(82, 191);
            ((System.ComponentModel.ISupportInitialize)(this.ToolSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button PencilToolBtn;
		private System.Windows.Forms.Label ToolBarLabel;
		private System.Windows.Forms.Button EraserToolBtn;
		private System.Windows.Forms.Button ColorPickerBtn;
		private System.Windows.Forms.Button ZoomInBtn;
		private System.Windows.Forms.Button ZoomOutBtn;
        private System.Windows.Forms.NumericUpDown ToolSize;
    }
}
