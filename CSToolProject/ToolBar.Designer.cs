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
			// ToolBar
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ColorPickerBtn);
			this.Controls.Add(this.EraserToolBtn);
			this.Controls.Add(this.ToolBarLabel);
			this.Controls.Add(this.PencilToolBtn);
			this.Name = "ToolBar";
			this.Size = new System.Drawing.Size(82, 104);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button PencilToolBtn;
		private System.Windows.Forms.Label ToolBarLabel;
		private System.Windows.Forms.Button EraserToolBtn;
		private System.Windows.Forms.Button ColorPickerBtn;
	}
}
