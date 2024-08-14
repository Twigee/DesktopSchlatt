using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace TransparentFormApp
{

    public class chatBoxclass()
    {
        Image image;
        Point Point;
    }
   public partial class chatMangement
    {
        public Stopwatch letterStopWatch;
        public string[] lines;
        public int randomLine;
        public Image chatbox;
        public string currentLine;
        string chatLine;
        Font font;
        public bool canDraw = false;
        List<chatBoxclass> chatBoxclassesItem = new();
        Schlatty schlatty ;
        public void Init()
        {
            readTxt();
            chatbox = Image.FromFile("ChatboxLarger.png");

            chatbox.RotateFlip(RotateFlipType.Rotate180FlipY);
            font = new Font(FontFamily.GenericMonospace, 20, FontStyle.Regular);
            
            schlatty = new Schlatty();
            letterStopWatch = new Stopwatch();
            letterStopWatch.Start();
            schlatty.fuck = true;   

        }



        public void createChatBox()
        {
            chatBoxclassesItem.Add(new chatBoxclass());
            pickRandom();
        }





        public void readTxt()
        {
          string Importlines = File.ReadAllText("Messages.txt");
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

            if (currentLine != null)
            {
                switch (currentLine)
                {
                    case "Hi! I'm schlatt, your\ndesktop personal assistant.":
                        SoundPlayer simpleSound = new SoundPlayer(@"Audio\Greating.wav");
                        simpleSound.Play();
                        break;



                    case "damn":
                        SoundPlayer simpleSound1 = new SoundPlayer(@"Audio\damn.wav");
                        simpleSound1.Play();
                        break;
                }


                canDraw = true;

            }
            else
            {
                pickRandom();
            }


        }
        public void removeMessage()
        {
            canDraw = false;
        }


    }
}
