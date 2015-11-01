using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace pixel_monitor
{
    public class ImageComparer
    {
        /// <summary>
        /// Compares a given color (r,g,b) value to another (r,g,b) value and if the color difference is at least mindelta returns true;
        /// </summary>
        /// <param name="current"></param>
        /// <param name="previous"></param>
        /// <param name="MinDelta"></param>
        /// <returns></returns>
        public bool PixelChanged(Color current, Color previous, int MinDelta)
        {
            int totalDelta = Math.Abs((current.R - previous.R) + (current.G - previous.G) + (current.B - previous.B));
            return totalDelta >= MinDelta;
        }

        public bool PixelsChanged(Color[] current, Color[] previous, int MinPixelDiffToBeConsideredDifferent, int MinDifferentPixels)
        {
            if(current.Length != previous.Length)
            {
                throw new Exception("Current and Previous color arrays are of differing lengths; could cause false positives.");
            }

            int totalPixelsChanged = 0;
            for(int i = 0; i < current.Length; i++)
            {
                if(PixelChanged(current[i], previous[i], MinPixelDiffToBeConsideredDifferent))
                {
                    totalPixelsChanged++;
                }
                if (totalPixelsChanged >= MinDifferentPixels)
                {
                    return true;
                }
            }

            return false;

        }

    }
}
