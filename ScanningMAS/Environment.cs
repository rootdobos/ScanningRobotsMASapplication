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
        public List<string> NextOperation
        {
            get
            {
                List<string> output=new List<string>();
                lock (_locker)
                {
                    while (_Operations.Count>0)
                    {
                        output.Add(_Operations.Dequeue());
                    }
                }
                return output;
            }
        }
        public Environment(string path)
        {
            _Map = new Bitmap(path);
            _MarkedPoints = new Bitmap(_Map.Width, _Map.Height);
            _MapContainer = new BitmapContainer(_Map);
        }
        public Environment(Bitmap map)
        {
            _Map = map;
            _MarkedPoints = new Bitmap(_Map.Width, _Map.Height);
            _MapContainer = new BitmapContainer(_Map);
        }
        public string ChangeEnvironment(string action)
        {
            lock (_locker)
            {
                _Operations.Enqueue(action);
            }
            return string.Empty;
        }
        public  void ProcessOperation(string action)
        {
            int x;
            int y;
            string color;
            string[] message = action.Split(':');
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
                catch (Exception e)
                {
                    ;
                }
            EventHandler<RefreshEventArgs> handler = Refresh;
            RefreshEventArgs args = new RefreshEventArgs();
            args.Locker = _locker;
            handler?.Invoke(this, args);
        }
        public string GetInformation(string query)
        {
            int x;
            int y;
            int radius;
            Communication.DecomposeSeeMessage(query,out x, out y, out radius);
            string output;
                output = ImageProcessing.GetVision(_MapContainer.ToBitmap(), x, y, radius);
            return output;
        }
        Bitmap _Map;
        Utilities.BitmapContainer _MapContainer;
        Bitmap _MarkedPoints;
        Queue<string> _Operations= new Queue<string>();
        object _locker = new object();
    }
}
