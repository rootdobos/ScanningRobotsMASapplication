using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class Communication
    {
        public static string ComposeSeeMessage(int x, int y, int radius)
        {
            return String.Format("X:{0}-Y:{1}-R:{2}", x, y, radius);
        }
        public static void DecomposeSeeMessage(string message,out int x, out int y, out int radius)
        {
            string[] components= message.Split(':');
            x = int.Parse(components[0].Split(':')[1]);
            y = int.Parse(components[1].Split(':')[1]);
            radius = int.Parse(components[2].Split(':')[1]);
        }
    }
}
