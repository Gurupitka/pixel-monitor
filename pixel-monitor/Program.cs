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
            //CheckScreenRegion(10, 10);
            Console.WriteLine("Press enter to begin monitoring");
            Console.ReadLine();

            Color previous;
            Color current;
            int XCoordinate = 300;
            int YCorordinate = 300;
            int MonitorFrequency = 3 * 1000; //3s
            int MinDeltaToAlertOn = 10;

            SoundPlayer SP = new SoundPlayer("C://Change.wav"); //change to whatever notify sound you want
            while (true)
            {

                previous = ImageComparer.GetCurrentRenderedPixelAtLocation(XCoordinate, YCorordinate);
                System.Threading.Thread.Sleep(MonitorFrequency);
                current = ImageComparer.GetCurrentRenderedPixelAtLocation(XCoordinate, YCorordinate);

                if (ImageComparer.PixelChanged(current, previous, MinDeltaToAlertOn))
                {
                    Console.WriteLine("SCREEN CHANGE DETECTED @ [" + System.DateTime.Now.ToString() + "]");
                    SP.Play();
                }

            }


        }

        /// <summary>
        /// just demonstrates how to check a screen region
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="Xorigin"></param>
        /// <param name="YOrigin"></param>
        static void CheckScreenRegion(int height, int width, int Xorigin = 0, int YOrigin = 0)
        {
            var colors = ImageComparer.GetCurrentRenderedPixelBox(height, width, Xorigin, YOrigin);
            System.Threading.Thread.Sleep(5000);
            var newcolors = ImageComparer.GetCurrentRenderedPixelBox(height, width, Xorigin, YOrigin);

            Console.WriteLine("Pixel box changed?" + ImageComparer.PixelsChanged(newcolors, colors, 1, 1));
        }
    }
}
