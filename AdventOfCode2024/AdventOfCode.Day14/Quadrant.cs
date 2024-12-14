using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day14
{
    internal record Quadrant
    {

        public int StartX { get; set; }
        public int StartY { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }
        public Quadrant(int startX, int startY, int endX, int endY)
        {
            StartX = startX;
            StartY = startY;
            EndX = endX;
            EndY = endY;
        }

        public IEnumerable<Robot> GetRobotsFromQuadrant(List<Robot> robots)
            => robots.Where(r => r.Position.x >= StartX &&  r.Position.y >= StartY && r.Position.x <= EndX && r.Position.y <= EndY);
    }
}
