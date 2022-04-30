using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Utilities
{
    public static class ImageProcessing
    {
        public static string GetVision(Bitmap image, int x, int y, int radius)
        {
            int minX = x - radius;
            int minY = y - radius;
            int maxX = x + radius;
            int maxY = y + radius;
            string output = "";
            for(int w=minX;w<maxX;w++)
            {
                for(int h=minY;h<maxX;h++)
                {
                    double dist = Math.Sqrt(Math.Pow((double)w-x,2)+ Math.Pow((double)h - y, 2));
                    if (dist<radius)
                    {
                        if (w < 0 || h < 0 || w >= image.Width || h >= image.Height)
                        {
                            output += String.Format("BOUNDARY:{0},{1}|", w, h);
                        }
                        else
                        {
                           Color pixelValues= image.GetPixel(w,h);
                            if(pixelValues.R<100)
                            {
                                output += String.Format("EDGE:{0},{1}|", w, h);
                            }
                        }
                    }
                }
            }
            return output;
        }
    }
}
