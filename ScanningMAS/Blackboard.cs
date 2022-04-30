using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiAgentSystem.Interfaces;
namespace ScanningMAS
{
    public class Blackboard:IBlackboard
    {
        public Blackboard()
        {
            _Positions = new Dictionary<string, Point>();
            _Edges = new List<Point>();
            _Boundaries = new List<Point>();
            _Steps = 0;
        }
        public string GetStatistics()
        {
            return string.Format("Agents{0} :::Steps Taken:{1}:::Edges Found:{2}",_Positions.Keys.Count, _Steps, _Edges.Count);
        }
        public void SetPosition(string name, Point position)
        {
            lock (_LockerPosition)
            {
                if (_Positions.ContainsKey(name))
                    _Positions[name] = position;
                else
                    _Positions.Add(name, position);
                _Steps++;
            }
        }
        public void AddEdge(Point position)
        {
            lock(_LockerEdges)
            {
                _Edges.Add(position);
            }
        }
        public void AddBoundary(Point position)
        {
            lock (_LockerBoundaries)
            {
                _Boundaries.Add(position);
            }
        }
        public List<Point> GetPositions()
        {
            List<Point> positions = new List<Point>();
            lock (_LockerPosition)
            {
                foreach(Point point in _Positions.Values)
                {
                    Point p = new Point(point.X, point.Y);
                    positions.Add(p);
                }
            }
            return positions;
        }
        public List<Point> GetEdges()
        {
            List<Point> positions = new List<Point>();
            lock (_LockerEdges)
            {
                foreach (Point point in _Edges)
                {
                    Point p = new Point(point.X, point.Y);
                    positions.Add(p);
                }
            }
            return positions;
        }
        public List<Point> GetBoundaries()
        {
            List<Point> positions = new List<Point>();
            lock (_LockerBoundaries)
            {
                foreach (Point point in _Boundaries)
                {
                    Point p = new Point(point.X, point.Y);
                    positions.Add(p);
                }
            }
            return positions;
        }
        Dictionary<string, Point> _Positions;
        List<Point> _Edges;
        List<Point> _Boundaries;
        int _Steps;
        private  object _LockerPosition=new object();
        private  object _LockerEdges=new object();
        private  object _LockerBoundaries=new object();
    }
}
