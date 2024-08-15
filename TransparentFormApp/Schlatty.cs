using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwigTools;

namespace TransparentFormApp
{

    enum shatStates
    {
        wandering,
        chating,
        sitting
    }

    public partial class Schlatty
    {
        Image hisImage;
        Image chatbox;

        public Point point;
        Point targetPoint = new Point();
        private Stopwatch walkWatch;




        int wanderingTime;
        int sittingTime;

        


        private int screenwidth;
        private int screenheight;
        private float lerpSpeed = 0.001f;
        private float t = 0;
        shatStates shatStates;
        chatMangement chat = new chatMangement();
        bool pickednewLines;

        bool timesSet = false;
        bool canproceed = false;
        List<int> whatToDo = new List<int>();
        Stopwatch taskTime = new Stopwatch();
        public int taskNumber;
        bool newSchedual = false;
        public bool fuck;
        bool destroyNewMessage;


        Image baseLayer;
        Image darkerLayer;
        Image arms;

        Bitmap bit;




        public void Init()
        {
            walkWatch = new Stopwatch();
            walkWatch.Start();
            hisImage = Image.FromFile("Assets\\Images\\a.jpg");

            baseLayer = Image.FromFile("Assets\\Images\\layer1.png");
            darkerLayer = Image.FromFile("Assets\\Images\\layer2.png");
            arms = Image.FromFile("Assets\\Images\\layer3.png");


            

            screenwidth = Screen.PrimaryScreen.Bounds.Width;
            screenheight = Screen.PrimaryScreen.Bounds.Height;
            point = new Point(screenwidth / 2, screenheight / 2); // Start at the center
            shatStates = new shatStates();
            
            pickednewLines = false;
            chat.Init();
            schedual();
            baseLayer = resizeImage(baseLayer, new Size(500,300));
            darkerLayer = resizeImage(darkerLayer, new Size(500, 300));
            arms = resizeImage(arms, new Size(500, 300));

           


           

        }



       






        public Image resizeImage(Image imgToResize, Size size)
        {
            Image image = imgToResize;

            bit = new Bitmap(image, size);
            return (Image)(bit);

            

        }

        public void Update(float deltaSeconds)
        {
            


            switch(taskNumber)
            {
                case 1: // Wandering
                    taskTime.Stop();

                    taskTime.Start();

                    if (taskTime.Elapsed.TotalSeconds <= wanderingTime)
                    {

                        if (walkWatch.Elapsed.TotalSeconds >= 7)
                        {
                            newpos();
                            walkWatch.Restart();
                        }
                        if (t < 1)
                        {
                            t += lerpSpeed * deltaSeconds; // Progress lerp parameter with speed factor
                            if (t > 1) t = 1; // Clamp to 1
                            point = Lerp.DoLerp(point, targetPoint, t);
                        }

                    }
                    else
                    {
                        taskTime.Reset();
                        schedual();
                    }
                    

                    break;


                    case 2: // Sitting


                    taskTime.Stop();
                    taskTime.Start();
                    if(taskTime.Elapsed.TotalSeconds<=sittingTime)
                    {

                    }
                    else
                    {
                        taskTime.Reset();
                        schedual();
                    }
                    
                    break;

                    case 3: // Has Something to say
                        taskTime.Stop();
                        taskTime.Start();
                        Console.WriteLine(taskTime.Elapsed.ToString());

                        if(taskTime.Elapsed.TotalSeconds<=6.5)
                        {
                            if(!pickednewLines)
                            {
                            chat.createMessage();
                            pickednewLines = true;
                            }
                            
                        }
                    else
                    {
                        pickednewLines = false;

                        taskTime.Reset();
                        chat.removeMessage();
                        schedual();
                    }









                    break;

            }
        }
        public void newpos()
        {
            Random random = new Random();

            int randomX = random.Next(0, screenwidth);
            int randomY = random.Next(0, screenheight);

            targetPoint = new Point(randomX, randomY);
            t = 0; // Reset the lerp parameter when a new position is generated
        }



        /// send keys 

        //public void sendKeysMethod()
        //{
        //    SendKeys.Send("w");
        //}


        public void schedual()
        {
            Random random = new Random();

            taskNumber = random.Next(1, 3);

            


            wanderingTime = random.Next(10, 30);
            sittingTime = random.Next(10, 13);

            newSchedual = true;
            Console.WriteLine("Clocked in and New things to do");
            Console.WriteLine("\n");
            DebugPrinter();
            
        }

        public void DebugPrinter()
        {
            switch (taskNumber)
            {
                case 1: Console.WriteLine("Current State: Wandering"); break;
                case 2: Console.WriteLine("Current State: Sitting"); break;
                case 3: Console.WriteLine("Current State: Chatting"); break;
            }
        }
        



        public void Draw(PaintEventArgs e)

        {


            using (Graphics g = Graphics.FromImage(bit))
            {
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            }


            Pen transparentPen = new Pen(Color.Transparent);
            
            e.Graphics.DrawRectangle(transparentPen, point.X, point.Y, hisImage.Width, hisImage.Height);
            //e.Graphics.DrawImage(chatbox, point.X + 160, point.Y - 235);
            e.Graphics.DrawImage(hisImage, point);

            //e.Graphics.DrawImage(baseLayer, 0, 0);

            //e.Graphics.DrawImage(darkerLayer, 0, 0);
            //e.Graphics.DrawImage(arms, 0, 0);


            //e.Graphics.DrawString(chat.currentLine, font, Brushes.Gray, point.X + 200, point.Y - 150);
            //if(chat.canDraw) e.Graphics.DrawImage(chat.chatbox, point.X + 160, point.Y - 235);
            chat.DrawChat(point, e, fuck);
        }

    }
}
