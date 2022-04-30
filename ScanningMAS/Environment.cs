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
        public event EventHandler<RefreshEventArgs> Refresh;
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
            lock (_locker)
            {
                try
                {

                    if (message[0] == "Mark")
                    {
                        Communication.DecomposeMarkPixel(action, out x, out y, out color);
                        if (color == "red")
                            _MarkedPoints.SetPixel(x, y, Color.Red);
                        else if (color == "green")
                            _MarkedPoints.SetPixel(x, y, Color.Green);
                        else if (color == "blue")
                            _MarkedPoints.SetPixel(x, y, Color.Blue);
                    }
                    else
                    {
                        Communication.DecomposeDeletePosition(action, out x, out y);
                        _MarkedPoints.SetPixel(x, y, Color.Transparent);
                    }
                }
                catch
                {
                    ;
                }
            }
            EventHandler<RefreshEventArgs> handler = Refresh;
            RefreshEventArgs args = new RefreshEventArgs();
            args.Locker = _locker;
            handler?.Invoke(this, args);
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
        object _locker = new object();
    }
}
