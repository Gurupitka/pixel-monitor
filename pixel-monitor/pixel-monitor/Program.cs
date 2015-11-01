using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace pixel_monitor
{
    class Program
    {
        static void Main(string[] args)
        {
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {

                    Color previous;
                    Color current;
                    Console.WriteLine("Press enter to begin monitoring");
                    Console.ReadLine();
                    int XCoordinate = 300;
                    int YCorordinate = 300;
                    int MonitorFrequency = 3 * 1000; //3s
                    int totalDelta = 0;
                    int MinDeltaToAlertOn = 10;
                    SoundPlayer SP = new SoundPlayer("C://Change.wav"); //change to whatever notify sound you want
                    while (true)
                    {

                        g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                        previous = bitmap.GetPixel(XCoordinate, YCorordinate);
                        System.Threading.Thread.Sleep(MonitorFrequency);

                        g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                        current = bitmap.GetPixel(XCoordinate, YCorordinate);
                        totalDelta = 0;

                        totalDelta += Math.Abs((current.R - previous.R) + (current.G - previous.G) + (current.B - previous.B));

                        if (totalDelta >= MinDeltaToAlertOn)
                        {
                            Console.WriteLine("SCREEN CHANGE DETECTED @ [" + System.DateTime.Now.ToString() + "]");
                            Console.WriteLine("Previous Screen = [" + previous.R + "," + previous.G + "," + previous.B + "]");
                            Console.WriteLine("Current Screen = [" + current.R + "," + current.G + "," + current.B + "]");
                            Console.WriteLine("Total Delta = [" + totalDelta + "]");
                            SP.Play();
                        }

                    }

                }

            }
        }
    }
}
