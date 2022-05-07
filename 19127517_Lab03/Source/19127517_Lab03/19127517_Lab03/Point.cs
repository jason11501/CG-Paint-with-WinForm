using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
namespace _19127517_Lab03
{
    class Point
    {
        public int X { get; set; }  //Tọa độ x
        public int Y { get; set; }  //Tọa độ y
        // Set tọa độ điểm
        public Point(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }
        public void setPoint(int x, int y)
        {
            X = x;
            Y = y;
        }
        public void setPoint(Point c)
        {
            X = c.X;
            Y = c.Y;
        }

        // Lấy tọa độ điểm
        public int getX() { return X; }
        public int getY() { return Y; }
    }
}
