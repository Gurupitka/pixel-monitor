using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using System.Collections.Generic;

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
        public static bool PixelChanged(Color current, Color previous, int MinDelta)
        {
            int totalDelta = Math.Abs((current.R - previous.R) + (current.G - previous.G) + (current.B - previous.B));
            return totalDelta >= MinDelta;
        }

        /// <summary>
        /// Simple method for comparing arrays of colors (pixels)
        /// </summary>
        /// <param name="current"></param>
        /// <param name="previous"></param>
        /// <param name="MinPixelDiffToBeConsideredDifferent"></param>
        /// <param name="MinDifferentPixels"></param>
        /// <returns></returns>
        public static bool PixelsChanged(Color[] current, Color[] previous, int MinPixelDiffToBeConsideredDifferent, int MinDifferentPixels)
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

        /// <summary>
        /// Gets the current rendered pixel at a given point.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Color GetCurrentRenderedPixelAtLocation(int x, int y)
        {
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            if (x > bounds.Width || y > bounds.Width || x < 0 || y < 0)
            {
                throw new Exception("Requested pixel is outside bounds of the screen.");
            }

            using (Bitmap bitmap = new Bitmap(1,1))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    Point sourcePoint = new Point(x, y);
                    Point destinationPoint = new Point(0, 0);
                    g.CopyFromScreen(sourcePoint, destinationPoint, new Size(1,1));
                    Color result = bitmap.GetPixel(0,0);
                    return result;
                }
            }
        }

        /// <summary>
        /// Gets the array of pixels representing the box speicified by the parameters
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="xOffset"></param>
        /// <param name="yOffset"></param>
        /// <returns></returns>
        public static Color[] GetCurrentRenderedPixelBox(int height, int width, int xOffset = 0, int yOffset = 0)
        {
            using (Bitmap bitmap = new Bitmap(width, height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    Size size = new Size(width,height);
                    Point offset = new Point(xOffset, yOffset);
                    g.CopyFromScreen(offset, offset, size);

                    Color[] pixels = new Color[height * width];
                    int index = 0; 
                    for(int x = 0; x < width; x++)
                    {
                        for(int y= 0; y < height; y++)
                        {
                            pixels[index] = bitmap.GetPixel(x, y);
                            index++;
                        }
                    }

                    return pixels;
                }
            }    
        }
    }
}
