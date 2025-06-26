using System.Windows.Forms;

namespace black {
	partial class main {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent( ) {
	   this.components = new System.ComponentModel.Container();
	   this.label5 = new System.Windows.Forms.Label();
	   this.timer1 = new System.Windows.Forms.Timer(this.components);
	   this.label3 = new System.Windows.Forms.Label();
	   this.SuspendLayout();
	   // 
	   // label5
	   // 
	   this.label5.BackColor = System.Drawing.Color.Bisque;
	   this.label5.Font = new System.Drawing.Font("Arial Black", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
	   this.label5.ForeColor = System.Drawing.Color.DarkBlue;
	   this.label5.Location = new System.Drawing.Point(1, 35);
	   this.label5.Margin = new System.Windows.Forms.Padding(0);
	   this.label5.Name = "label5";
	   this.label5.Size = new System.Drawing.Size(75, 26);
	   this.label5.TabIndex = 15;
	   this.label5.Text = "00:00";
	   this.label5.Click += new System.EventHandler(this.label5_Click);
	   // 
	   // timer1
	   // 
	   this.timer1.Enabled = true;
	   this.timer1.Interval = 600;
	   this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
	   // 
	   // label3
	   // 
	   this.label3.BackColor = System.Drawing.SystemColors.ButtonFace;
	   this.label3.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
	   this.label3.ForeColor = System.Drawing.Color.DarkRed;
	   this.label3.Location = new System.Drawing.Point(1, 4);
	   this.label3.Name = "label3";
	   this.label3.Size = new System.Drawing.Size(48, 26);
	   this.label3.TabIndex = 16;
	   this.label3.Text = "45";
	   this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
	   // 
	   // main
	   // 
	   this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
	   this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
	   this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
	   this.ClientSize = new System.Drawing.Size(127, 73);
	   this.ControlBox = false;
	   this.Controls.Add(this.label3);
	   this.Controls.Add(this.label5);
	   this.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
	   this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
	   this.Location = new System.Drawing.Point(1, 1055);
	   this.Margin = new System.Windows.Forms.Padding(6);
	   this.MaximizeBox = false;
	   this.MinimizeBox = false;
	   this.Name = "main";
	   this.Opacity = 0.8D;
	   this.ShowIcon = false;
	   this.ShowInTaskbar = false;
	   this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
	   this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
	   this.TopMost = true;
	   this.TransparencyKey = System.Drawing.Color.White;
	   this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Timer timer1;
		private Label label3;
	}
}