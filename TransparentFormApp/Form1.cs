using System;
using System.Diagnostics;
using System.Drawing;
using TwigTools;
using System.Windows.Forms;
using System.Media;
using System.Runtime.InteropServices;
using System.Drawing.Printing;

namespace TransparentFormApp
{
    public partial class Form1 : Form
    {


        Schlatty schlatty = new Schlatty();

        public System.Windows.Forms.Timer gametime;


        private Stopwatch gamewatch = new Stopwatch();

        private TimeSpan lastFrameTime;

        private TimeSpan deltaTime;

        float deltaSeconds;

        public SoundPlayer snd;

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        public Form1()
        {

            InitializeComponent();
            this.TopMost = true;

            this.BackColor = Color.Black;
            this.TransparencyKey = Color.Black;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

           

            gamewatch.Start();
            lastFrameTime = gamewatch.Elapsed;
            gametime = new System.Windows.Forms.Timer();
            gametime.Interval = 20;   // milliseconds
            gametime.Tick += Update;  // set handler
            gametime.Start();

            this.KeyDown += new KeyEventHandler(this.Form1_KeyDown);


            schlatty.Init();
            //AllocConsole();
            Debug.WriteLine("Init");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This is debug stuff, please ignore!");
            Console.ForegroundColor = ConsoleColor.White;



            this.DoubleBuffered = true;

        }

        public void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                schlatty.taskNumber = 3;
            }

            if (e.KeyCode == Keys.P)
            {
                
            }

            

            

        }

      


        private void Update(object sender, EventArgs e)
        {
            TimeSpan currentFrameTime = gamewatch.Elapsed;
            deltaTime = currentFrameTime - lastFrameTime;
            lastFrameTime = currentFrameTime;
            deltaSeconds = (float)deltaTime.TotalSeconds;


            //Console.WriteLine(TwigMath.Distance(Control.MousePosition, schlatty.point));


            schlatty.Update(deltaSeconds);

            this.Invalidate();
        }

        

        protected override void OnPaint(PaintEventArgs e)
        {

            base.OnPaint(e);
            schlatty.Draw(e);


        }
    }
}
