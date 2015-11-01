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
    }
}
