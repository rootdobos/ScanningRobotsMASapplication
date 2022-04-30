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
        public Bitmap MarkedPoint
        {
            get
            {
                return _MarkedPoints;
            }
        }
        public Bitmap Map
        {
            get
            {
                return _Map;
            }
        }
        public Environment(string path)
        {
            _Map = new Bitmap(path);
            _MarkedPoints = new Bitmap(_Map.Width, _Map.Height);
        }
        public Environment(Bitmap map)
        {
            _Map = map;
            _MarkedPoints = new Bitmap(_Map.Width, _Map.Height);
        }
        public string ChangeEnvironment(string action)
        {
            int x;
            int y;
            string color;
            string[] message = action.Split(':');
            if (message[0] == "Mark")
            {
                Communication.DecomposeMarkPixel(action, out x, out y, out color);
                _MarkedPoints.SetPixel(x, y, Color.Green);
            }
            else
            {
                Communication.DecomposeDeletePosition(action, out x, out y);
                _MarkedPoints.SetPixel( x, y, Color.Transparent);
            }

            return string.Empty;
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
        Bitmap _MarkedPoints;
    }
}
