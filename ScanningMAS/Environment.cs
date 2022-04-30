using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiAgentSystem.Interfaces;
using System.Drawing;
using Utilities;
namespace ScanningMAS
{
    public class Environment : IEnvironment
    {
        public Environment(string path)
        {
            _Map = new Bitmap(path);
        }
        public Environment(Bitmap map)
        {
            _Map = map;
        }
        public string ChangeEnvironment(string query)
        {
            throw new NotImplementedException();
        }

        public string GetInformation(string query)
        {
            int x;
            int y;
            int radius;
            Communication.DecomposeSeeMessage(query,out x, out y, out radius);
            return ImageProcessing.GetVision(_Map, x, y, radius);
        }
        Bitmap _Map;
    }
}
