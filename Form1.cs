using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Media;
using System.Runtime.CompilerServices;

namespace black {
	public partial class Form1 : Form {
		#region   变量 。。。。。。。。
		[DllImport( "user32.dll" )]
		static extern void BlockInput(bool Block);
		// 将以下字段设置为只读，因为它们在代码中未被重新分配  
		private SoundPlayer sp;
		bool isright = false;   //鼠标翻页用
		private string path = @"D:\文档\My Pictures\WOMEN";
		string myname, mfile;
		const int MinLEN = 1000 * 60;    //一分钟  工作时间 定时	const int MinLEN = 1000; 
		List<string> imageList = new List<string>();
		int count = 1;  //计数
		int delatcount = 3000;          //2000ms
		public int locationx, locationy, timex, timey;  //下脚时间显示XY 坐标
		int listtime = 0;                //显示帮助10秒
		int tempint = 0;      //     bool bclose = false;    //      bool dispbg = true;
		bool closekey = false;   //close key  mouse //       bool openkey = true;   //定时打开
		int hh, ww;
		char music = 'N';
		private readonly string strfile = @"d:\readme.txt";
		private readonly string alert = @"d:\提醒.wav";

		#endregion
		private void Readfile( ) {
			timer1.Enabled = false;
			string[] strs = File.ReadAllLines( strfile, Encoding.Default );  //read file 内容
			imageList.Clear(); tempint = strs.Length;
			if (tempint == 0) {
				DialogResult dr = MessageBox.Show( "没在readme.txt文件中存入任何资源！", "提示信息", MessageBoxButtons.OK );
				if (dr == DialogResult.OK) {
					Thread.Sleep( 1000 ); Environment.Exit( 1 );
				}
			}
			char[] ss = new char[100];
			try {
				for (int i = 0; i < strs.Length; i++) {
					path = strs[i];                        //	var timagefiles = new string[9000];
					if (path[0] != '*') {
						try {
							var timagefiles = Directory.GetFiles( path, "*.*" ).Where( file => file.ToLower().EndsWith( "bmp" ) || file.ToLower().EndsWith( "jpeg" ) || file.ToLower().EndsWith( "png" ) || file.ToLower().EndsWith( "jpg" ) || file.ToLower().EndsWith( "gif" ) ).ToList();
							imageList.AddRange( timagefiles );
						} catch (Exception e) {
							Console.WriteLine( $"Processing failed: {e.Message}" );         //		LogError( e, "Shape processing failed." );
							throw;
						}

					} else {
						ss = path.ToCharArray(); ss[0] = '0';
						char S = ss[1]; int lenc = path.Length - 2;
						char[] nss = new char[lenc];
						if (S == 'X' || S == 'N' || S == 'Y' || S == 'D' || S == 'M' || S == 'L' || S == 'S' || S == 'H' || S == 'W' || S == 'T') {
							for (int ci = 0; ci < lenc; ci++) nss[ci] = ss[ci + 2];
							path = new string( nss );
							switch (S) {
								case 'X': locationx = Convert.ToInt32( path ); break;
								case 'Y': locationy = Convert.ToInt32( path ); break;
								case 'W': timex = Convert.ToInt32( path ); break;
								case 'T': timey = Convert.ToInt32( path ); break;
								case 'S': A.dels = Convert.ToInt32( path ); break;
								case 'N':
									music = ss[2];
									if (music == 'N') {

									}
									break;
								case 'L':
									A.deltime = Convert.ToInt32( path );
									//  label3.Text = A.deltime.ToString(); 
									A.deltimec = A.deltime; break;
								case 'M': mfile = path; break;
								case 'D': if (nss[0] != ':') delatcount = Convert.ToInt32( path ); break;
								default: break;
							}
						}
					}
				}
			} catch (IOException e) {
				// Extract some information from this exception, and then 
				// throw it to the parent method.
				if (e.Source != null)
					MessageBox.Show( e.ToString() );    //                throw;
				Application.Exit(); Environment.Exit( 1 );
			}
			if (imageList.Count == 0) {
				DialogResult dr = MessageBox.Show( "没在readme.txt文件中存入要显示的图片的目录！", "提示信息", MessageBoxButtons.OK );
				if (dr == DialogResult.OK) {
					Thread.Sleep( 1000 );
					Environment.Exit( 1 );
				}
			}
			//	timer1.Enabled = true;  关显示
		}
		public Form1( ) {
			InitializeComponent();
			Readfile();
			this.Location = new Point( locationx, locationy );
			//Rectangle ScreenArea1 = Screen.GetBounds( this );      //width1 = ScreenArea1.Width; //屏幕宽度    //height1 = ScreenArea1.Height; //屏幕高度   //this.Size = new Size( width1, height1 );
			//     pictureBox1.Size = new Size( width1, height1 );     //    pictureBox1.Location = new Point( 0, 0 );   // Point( BX, 1 );
			timer2.Enabled = false;
			timer3.Interval = MinLEN;         //			musicplay mus = new musicplay();
			sp = new SoundPlayer();
			var System = new main { Location = new Point( -83, 1085 ) };  //定位显示倒计时
			System.Show();

		}
		#region  不用 程序 List<string> GetAllFiles button1_Click
		static List<string> GetAllFiles(string path) {
			List<string> ret = new List<string>();
			ret.AddRange( Directory.GetFiles( path ) );
			Array.ForEach( Directory.GetDirectories( path ),
				delegate (string path1) { ret.AddRange( GetAllFiles( path1 ) ); } );
			return ret;

			//foreach (string subDirPath in Directory.GetDirectories( path ))
			//{
			//    ret.AddRange( GetAllFiles( subDirPath ) );
			//    myname = subDirPath;
			//}
		}
		private void Button1_Click(object sender, EventArgs e) {
			//设置打开图像的类型
			//        openFileDialog1.Filter = " *.jpg,*.jpeg,*.bmp,*.jif,*.ico,*.png,*.tif,*.wmf|*.jpg;*.jpeg;*.bmp;*.gif;*.ico;*.png;*.tif;*.wmf";
			//  openFileDialog1.ShowDialog();    //“打开”对话框  "F:\\微云同步盘\\图片\\18[1].jpg"
			//myname = openFileDialog1.FileName;

			//      DirectoryInfo theFolder = new DirectoryInfo( @"F:\微云同步盘\图片\" );

			//      DirectoryInfo[] dirInfo = theFolder.GetDirectories();
			//      //遍历文件夹
			//      //      ListBox listBox2 = new ListBox();

			////      FileInfo[] fileInfo = dirInfo.GetFiles();

			//      foreach (FileInfo NextFile in fileInfo)  //遍历文件
			//          this.listBox2.Items.Add( NextFile.Name );

			//      foreach (DirectoryInfo NextFolder in dirInfo)
			//      {
			//           this.listBox1.Items.Add(NextFolder.Name);
			//          FileInfo[] fileInfo = NextFolder.GetFiles();

			//      }

			//DirectoryInfo dir = new DirectoryInfo( "c:\path" ); string[] files = dir.GetFiles();
			//foreach (string file in files)
			//{    //就是获得文件路径信息中除了文件扩展名之外的那部分---也就是 文件名了.  
			//    comboboxObj.Items.Add(Path.GetFileNameWithoutExtension(file));
			//}
			//foreach (ArrayList ret in myname)
			//{
			//    pictureBox1.Image = Image.FromFile( myname );  //显示打开图像
			//}  ret.Count;
			//    string str="";
			//    for (int i = 0; i < 4;  i++)
			//    {
			//         str = ret[i].ToString();
			//        pictureBox1.Image = Image.FromFile( str );  //显示打开图像
			//        int h = pictureBox1.Image.Height;
			//        int w = pictureBox1.Image.Width;

			//    //   Thread.Sleep( 2000 );//休眠时间
			//    }
			////    pictureBox1.Image = Image.FromFile( str );  //显示打开图像

		}
		private void Button2_Click(object sender, EventArgs e) {
			if (pictureBox1.Width >= 50)   //当图像的宽度值小于50时，就不能再缩小了
			{
				pictureBox1.Width = Convert.ToInt32( pictureBox1.Width * 0.8 );
				pictureBox1.Height = Convert.ToInt32( pictureBox1.Height * 0.8 );
			} else {
				MessageBox.Show( this, "图像已是最小，不能再缩小了！", "提示对话框", MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
		}
		private void Button3_Click(object sender, EventArgs e) {
			if (pictureBox1.Width < 310)    //当图像的宽度值大于310时，就不能再放大了
			{
				pictureBox1.Width = Convert.ToInt32( pictureBox1.Width * 1.2 );
				pictureBox1.Height = Convert.ToInt32( pictureBox1.Height * 1.2 );
			} else {
				MessageBox.Show( this, "图像已是最大，不能再放大了！", "提示对话框", MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
		}
		#endregion
		#region  Form1_Load
		private void Form1_Load(object sender, EventArgs e) {
			Random rd = new Random(); count = rd.Next( 1, (imageList.Count - 1) );   //          count=1;
			timer1.Interval = delatcount; label4.Visible = false; label2.Visible = false; label1.Visible = false;
			//       Rectangle ScreenArea = Screen.GetWorkingArea( this );这个区域包括任务栏，就是屏幕显示的物理范围
			myname = imageList[count].ToString();
			string sPicPaht = myname.ToString();
			Bitmap bmPic = new Bitmap( sPicPaht );
			Point ptLoction = new Point( bmPic.Size );
			pictureBox1.LoadAsync( sPicPaht );
			count++;
			#endregion
			#region  副屏
			//         BackgroundImage = Image.FromFile( @"D:\gxh.jpg" );
			//this.StartPosition=FormStartPosition.Manual;    //副屏
			//Screen[] screens = System.Windows.Forms.Screen.AllScreens;
			//if (screens.Length>=2) {
			//    for (int i = 0; i<screens.Length; i++) {
			//        //获取扩展屏
			//        Screen childerScreen = screens[i];
			//        this.Width=childerScreen.Bounds.Width;
			//        this.Height=childerScreen.Bounds.Height;
			//        this.Location=childerScreen.WorkingArea.Location;
			//    }
			//}
			//	Form2 fm = new Form2();
			//	fm.Location = new Point( 3441, 346 );
			//	fm.Show();

		}
		#endregion
		//定时 显示
		private void Timer1_Tick(object sender, EventArgs e) {
			if (!closekey) {
				myname = imageList[count].ToString();
				try {     //                        pictureBox1.Location=new Point(0, 0);   // Point( BX, 1 );   pictureBox1.Width
					string sPicPaht = myname.ToString();
					Bitmap bmPic = new Bitmap( sPicPaht );
					Point ptLoction = new Point( bmPic.Size );
					hh = pictureBox1.Image.Height; ww = pictureBox1.Image.Width;
					pictureBox1.LoadAsync( sPicPaht );     //LoadAsync：非同步載入圖像   
																		//	hh = pictureBox1.Image.Height;	ww = pictureBox1.Image.Width;
				} catch (ArgumentException) { // label1.Visible = true;  // label1.Text = .Message.ToString();
					throw new ArgumentException();
				}
				count++; if (count >= imageList.Count) count = 1;
			}
		}
		//键盘按键
		private void Form1_KeyDown(object sender, KeyEventArgs e) {
			Keys key = e.KeyCode;    //  if (e.Control != true)//如果没按Ctrl键      return;
			switch (key) {
				case Keys.F1:
					timer2.Enabled = true;
					richTextBox1.Visible = true;
					richTextBox1.Refresh();
					listtime = 10;  //给显示时间
					break;
				case Keys.F2:
					if (pictureBox1.Visible == true) {
						pictureBox1.Visible = false;
						timer1.Enabled = false;
						pictureBox1.Image = Image.FromFile( "d:\\gxh.jpg" );
					} else {
						pictureBox1.Visible = true;
						timer1.Enabled = true;
						myname = imageList[count].ToString();
						pictureBox1.Image = Image.FromFile( myname );               //                  this.pictureBox1.Size = new Size(1920,1080);
					}
					break;
				case Keys.F3:
					if (timer1.Enabled) {
						string str = "宽度：" + ww.ToString(); label2.Text = str;
						str = "高度：" + hh.ToString(); label4.Text = str;
						label1.Text = myname;
						timer1.Enabled = false;
						label4.Visible = true;
						label2.Visible = true;
						label1.Visible = true;
					} else {
						label4.Visible = false;
						label2.Visible = false;
						label1.Visible = false;
						timer1.Enabled = true;
					}
					break;
				case Keys.F4:             //减少 图片显示时间5秒
					delatcount -= 2000;
					if (delatcount <= 0) delatcount = 500;
					timer1.Interval = delatcount;
					break;
				case Keys.F6:             //停音乐
					if (music == 'Y') {
						music = 'N'; sp.Stop();
					} else {
						music = 'Y'; sp.SoundLocation = mfile; sp.PlayLooping();
					}
					break;
				case Keys.F5:             //增加图片显示时间
					delatcount += 2000; timer1.Interval = delatcount; break;

				case Keys.F11:             //关闭计算机
					Shutdown f = new Shutdown {                                    //窗口定位到左中
						Location = new Point( locationx, locationy )
					};
					//		f.StartPosition;
					f.ShowDialog(); break;

				case Keys.F12: Application.Exit(); pictureBox1.Dispose(); this.Close(); break;
				case Keys.F10: Application.Exit(); pictureBox1.Dispose(); this.Close(); break;
			}
		}
		//关闭 F1 显示
		private void Timer2_Tick(object sender, EventArgs e) {
			if (listtime > 0) {
				listtime--;
				if (listtime == 0)
					richTextBox1.Visible = false;
			} else
				timer2.Enabled = false;
		}
		//鼠标按键
		private void PictureBox1_MouseClick(object sender, MouseEventArgs e) {
			timer1.Enabled = false;
			FileStream fileStream = new FileStream( myname, FileMode.Open, FileAccess.Read );
			if (e.Button == MouseButtons.Left) {
				try {
					if (isright) {
						count++; if (count >= imageList.Count) count = 1; isright = false;
					}
					myname = imageList[count].ToString();
					fileStream = new FileStream( myname, FileMode.Open, FileAccess.Read );
					pictureBox1.Image = Image.FromStream( fileStream );
					hh = pictureBox1.Image.Height; ww = pictureBox1.Image.Width;
				} catch { } finally {
					count++; if (count >= imageList.Count) count = 1;
				}
			} else if (e.Button == MouseButtons.Right) {
				if (!isright) {
					count--; if (count <= 0) count = 1; count--; if (count <= 0) count = 1;             //倒退三次
					isright = true;
				} else {
					count--; if (count <= 0) count = 1;   //连续向后翻页
				}
				myname = imageList[count].ToString();
				fileStream = new FileStream( myname, FileMode.Open, FileAccess.Read );
				pictureBox1.Image = Image.FromStream( fileStream );
				hh = pictureBox1.Image.Height; ww = pictureBox1.Image.Width;
			}
			fileStream.Close();     //释放内存
			fileStream.Dispose();
			string str = "宽度：" + ww.ToString(); label2.Text = str;
			str = "高度：" + hh.ToString(); label4.Text = str; label1.Text = myname;
			timer1.Interval = delatcount; timer1.Enabled = true;
		}
		private void Form1_Paint(object sender, PaintEventArgs e) {
			//Image newImage = Image.FromFile( "C:\\gxh.jpg" );
			//Point ulCorner = new Point( 0, 0 );
			//e.Graphics.DrawImage( newImage, ulCorner );     // Draw image to screen.
		}
		//显示 右下角流水时间
		//private void Timer3_Tick(object sender, EventArgs e) {
		//   if (A.shuttimet) {   //定时关机
		//	pictureBox1.Visible = false;
		//	timer1.Enabled = false;
		//	closekey = true;
		//   }
		//   else {
		//	pictureBox1.Visible = true;
		//	timer1.Enabled = true;
		//	closekey = false;
		//   }
		//   label3.BringToFront(); label5.BringToFront(); //显示左上角的 当前时间 和 本次运行时间
		//   DateTime dt = DateTime.Now.AddMilliseconds( 0 - Environment.TickCount );   //计算 开机时间
		//   TimeSpan m_WorkTimeTemp = new TimeSpan(Convert.ToInt64(Environment.TickCount) * 10000);
		//   label5.Text = m_WorkTimeTemp.Hours + ": " + m_WorkTimeTemp.Minutes;
		//   label3.Text = A.deltime.ToString();
		//   A.deltime--;

		//   if (A.deltime <= 0) {
		//	if (!closekey) {                  //当前 关键盘
		//	   if (music == 'Y') {
		//		sp.SoundLocation = mfile; sp.PlayLooping();    //sp.Play();
		//	   }
		//	   else {
		//		sp.SoundLocation = alert; sp.PlayLooping();
		//		Thread.Sleep(900); //休眠 100毫秒
		//		sp.Stop();
		//	   }
		//	   pictureBox1.Visible = false;
		//	   A.deltime = A.dels;
		//	   BlockInput(true);           //锁定鼠标及键盘
		//	   closekey = true;
		//	}
		//	else {
		//	   sp.Stop();
		//	   pictureBox1.Visible = true;
		//	   A.deltime = A.deltimec;
		//	   BlockInput(false);           //打开鼠标及键盘
		//	   closekey = false;
		//	}
		//   }
		//}
		private void Timer3_Tick(object sender, EventArgs e) {   //显示时间
			if (A.shuttimet) {   //定时关机
				pictureBox1.Visible = false;
				timer1.Enabled = false;
				closekey = true;
			}

			label3.BringToFront(); label5.BringToFront();
			_ = DateTime.Now.AddMilliseconds( 0 - Environment.TickCount );   //计算 开机时间
			TimeSpan m_WorkTimeTemp = new TimeSpan( Convert.ToInt64( Environment.TickCount ) * 10000 );
			label5.Text = m_WorkTimeTemp.Hours + ": " + m_WorkTimeTemp.Minutes;
			//	label5.to
			label3.Text = A.deltime.ToString(); A.deltime--;
			if (A.deltime <= 0) {
				if (!closekey) {                  //当前 关键盘
					if (music == 'Y') {
						sp.SoundLocation = mfile; sp.PlayLooping();    //sp.Play();
					} else {
						sp.SoundLocation = alert; sp.Play();
					}
					pictureBox1.Visible = false;
					A.deltime = A.dels;      //确定延时时间
					BlockInput( true );           //锁定鼠标及键盘
					closekey = true;
					Thread.Sleep( 1000 * 2 ); sp.Stop();
				} else {
					//  sp.SoundLocation = endalert;
					//  if (musiccount != 0) {
					//sp.SoundLocation = musicList[musiccountint++].ToString();
					//if (musiccountint > musiccount) musiccountint = 0;
					//  }
					sp.Play();
					pictureBox1.Visible = true;
					A.deltime = A.deltimec;        //回复给定 工作时间
					BlockInput( false );           //打开鼠标及键盘
					closekey = false;                 //开关
																 //Thread.Sleep(1000 * musictime);
					sp.Stop();  //音乐播放持续播放
				}
			}
		}     //冒泡排序函数


	}





	class A {
		public static int deltime, deltimec, dels, shuttime; //    delay  50 mins
		public static bool shuttimet = false;
		public static bool start = false;

	}
}
