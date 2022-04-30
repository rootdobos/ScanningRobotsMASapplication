using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiAgentSystem.Interfaces;
using Utilities;

namespace ScanningMAS
{
    public class SteppingAction : IAction
    {
        public SteppingAction(List<Point> agentPositions,List<Point> edgePositions, List<Point> boundaryPosition)
        {
            _agentPositions = agentPositions;
            _edgePositions = edgePositions;
            _boundaryPosition = boundaryPosition;
        }
        public void execute(IAgent agent, string perception)
        {
            Point agentPosition = ((ScanningAgent)agent).Position;

            Point newPos = CalculateDirection(agentPosition);

            if (!ImageProcessing.IsObject(newPos, _edgePositions, _boundaryPosition))
                ((ScanningAgent)agent).Position = newPos;
        }

        private Point CalculateDirection(Point agentPosition)
        {
            double boundaryDirX = 0;
            double boundaryDirY = 0;
            foreach(Point p in _boundaryPosition)
            {
               double distance=factor* EucledianDistance(p, agentPosition);
                Point dir = Direction(agentPosition, p);
                boundaryDirX += distance * dir.X;
                boundaryDirY += distance * dir.Y;
            }
            foreach (Point p in _edgePositions)
            {
                double distance = factorEdge * EucledianDistance(p, agentPosition);
                Point dir = Direction(agentPosition, p);
                boundaryDirX += distance * dir.X;
                boundaryDirY += distance * dir.Y;
            }
            Random random = new Random();
            int x = random.Next(randomness);
            int y = random.Next(randomness);
            Point newPos = new Point();
            if(x>=randomness-2)
                newPos.X = agentPosition.X + GetSign(boundaryDirX);
            else
                newPos.X = agentPosition.X - GetSign(boundaryDirX);
            if (y >= randomness - 2)
                newPos.Y = agentPosition.Y + GetSign(boundaryDirY);
            else
                newPos.Y = agentPosition.Y - GetSign(boundaryDirY);
            //newPos.X = agentPosition.X + (x - 1);
            //newPos.Y = agentPosition.Y + (y - 1);
            return newPos;
        }
        private double EucledianDistance(Point A, Point B)
        {
            return Math.Sqrt(Math.Pow((double)A.X - B.X, 2) + Math.Pow((double)A.Y - B.Y, 2));
        }
        private Point Direction(Point A, Point B)
        {
            Point output = new Point();
            if (A.X > B.X)
                output.X = 1;
            else if (A.X < B.X)
                output.X = -1;
            else 
                output.X =0;

            if (A.Y > B.Y)
                output.Y = 1;
            else if (A.Y < B.Y)
                output.Y = -1;
            else
                output.Y = 0;
            return output;
        }
        private int GetSign(double number)
        {
            if (number >= 0)
                return 1;
            else
                return -1;
        }
        List<Point> _agentPositions;
        List<Point> _edgePositions;
        List<Point> _boundaryPosition;
        double factor = 1;
        double factorEdge = 1 ;
        int randomness =6;
    }
}
