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
                        if (CheckBoundary(w,h,image.Width,image.Height))
                        {
                            output += String.Format("BOUNDARY:{0},{1}|", w, h);
                        }
                        else if(w >0 && h > 0 && w < image.Width - 1 && h < image.Height - 1)
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
        private static bool CheckBoundary(int w, int h,int width,int height)
        {
            if (w == 0 && h > 0 && h < height - 1)
                return true;
            if (w == width - 1 && h > 0 && h < height - 1)
                return true;
            if (h == 0 && w > 0 && w < width - 1)
                return true;
            if (h == height - 1 && w > 0 && w < width - 1)
                return true;
            return false;
        }
        public static bool IsObject(Point point, List<Point> edges, List<Point> boundaries)
        {
            foreach (Point edge in edges)
            {
                if (edge.X == point.X && edge.Y == point.Y)
                    return true;
            }
            foreach (Point boundary in boundaries)
            {
                if (boundary.X == point.X && boundary.Y == point.Y)
                    return true;
            }
            return false;
        }
    }
}
