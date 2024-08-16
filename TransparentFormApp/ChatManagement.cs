using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace TransparentFormApp
{

    public class chatBoxclass()
    {
        Image? image;
        Point? Point;
    }
   public partial class chatMangement
    {
        public Stopwatch? letterStopWatch;
        public string[]? lines;
        public int randomLine;
        public Image? chatbox;
        public string? currentLine;
        string? chatLine;
        Font? font;
        public bool canDraw = false;
        List<chatBoxclass> chatBoxclassesItem = new();
        Schlatty? schlatty;
        string? lastLine;

        bool CanPrint;
        bool canPlaySound;


        string[]? printList;
        public void Init()
        {
            readTxt();
            chatbox = Image.FromFile("Assets\\Images\\ChatboxLarger.png");

            chatbox.RotateFlip(RotateFlipType.Rotate180FlipY);
            font = new Font(FontFamily.GenericMonospace, 20, FontStyle.Regular);
            
            schlatty = new Schlatty();
            letterStopWatch = new Stopwatch();
            letterStopWatch.Start();
            schlatty.fuck = true;


            printList = Directory.GetFiles("Assets\\PrinterImages");
            
            foreach (string file in printList) { Console.WriteLine(file); }

            string configText = File.ReadAllText("Config.json");

            var configs = JsonSerializer.Deserialize<Configs>(configText);

            canPlaySound = configs.AllowPlaySound;
            Console.WriteLine(canPlaySound);
        }
        public void importValues(bool allowPrint, bool allowSnd)
        {
            CanPrint = allowPrint;
            canPlaySound = allowSnd;
        }


        public void createChatBox()
        {
            chatBoxclassesItem.Add(new chatBoxclass());
            pickRandom();
        }





        public void readTxt()
        {
          string Importlines = File.ReadAllText("Assets\\Messages.txt");
          lines = Importlines.Split(';');
        }


        public void pickRandom()
        {
            Random random = new Random();
            randomLine = random.Next(0, lines.Length);
            currentLine = lines[randomLine];
            currentLine = currentLine.Replace("\\n", "\n"); // I dont know why, frankly i really dont care why, when it takes in a \ it reads it as \\

        }

    

        

        public void DrawChat(Point point, PaintEventArgs f,bool pickednewLine)
        {
            if(canDraw)
            {
                f.Graphics.DrawImage(chatbox, point.X + 160, point.Y - 235);
                f.Graphics.DrawString(currentLine, font, Brushes.Gray, point.X + 200, point.Y - 150);


                //foreach (char c in currentLine)
                //{
                //    if (letterStopWatch.Elapsed.TotalSeconds >= 0.08)
                //    {
                //        chatLine = chatLine += c.ToString();
                //        letterStopWatch.Restart();
                //    }


                //}
            }






        }
        public void createMessage()
        {
            pickRandom();
            Console.WriteLine(currentLine);
            if (currentLine != lastLine)
            {
                switch (currentLine)
                {
                    case "Hi! I'm schlatt, your\ndesktop personal assistant.":
                        if (canPlaySound) { SoundPlayer simpleSound = new SoundPlayer(@"Assets\\Audio\\Greating.wav"); simpleSound.Play(); }

                        break;



                    case "damn":
                        if(canPlaySound)
                        {
                            SoundPlayer simpleSound1 = new SoundPlayer(@"Assets\\Audio\\damn.wav");
                            simpleSound1.Play();
                        }
                        
                        break;

                    case "":
                        createMessage();
                        break;
                    case "Check your printer":
                        if(CanPrint)
                        {
                            // i do not want to be printing 9/11 gay jokes to my printer at 3am

                            PrintDocument pd = new PrintDocument();
                            pd.PrintPage += printImg;
                            Console.WriteLine("printing Image!");
                            pd.DefaultPageSettings.Color = false;

                            pd.Print();
                        }
                        else if(!CanPrint)
                        {
                            createMessage();
                        }
                        break;
                }

                canDraw = true;
            }
            else createMessage();

           

            
           


        }
        public void removeMessage()
        {
            canDraw = false;
            lastLine = currentLine;

        }

        private void printImg(object o, PrintPageEventArgs e)
        {
            Image img = Image.FromFile("PrinterImages\\90bhx4.jpg");
            Point pnt = new Point(100, 100);
            e.Graphics.DrawImage(img, pnt);
        }


    }
}
