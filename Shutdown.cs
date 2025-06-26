using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace black {
	public partial class Shutdown : Form {
		public Shutdown( ) {
			InitializeComponent();
			A.shuttime = 30;
			label2.Text = "30";
			timer1.Enabled = true;
			A.start = false;
		}

		private void Button1_Click(object sender, EventArgs e) {
			A.shuttime = Convert.ToInt32( textBox1.Text ); //			Form1 form1 = new Form1();
			label2.Text = A.shuttime.ToString();
			A.shuttimet = true;
		}

		private void Timer1_Tick(object sender, EventArgs e) {
			if (A.shuttimet) {
				if (A.shuttime != 0) {
					A.shuttime--;
					label2.Text = A.shuttime.ToString();
				} else {
					label2.Text = A.shuttime.ToString();
			//	Process.Start( "shutdown", "/s /t 0" );    // 参数 /s 的意思是要关闭计算机
				}
			}
			
		}

		private void button2_Click(object sender, EventArgs e) {
			A.shuttimet = false;
		}
	}
}
