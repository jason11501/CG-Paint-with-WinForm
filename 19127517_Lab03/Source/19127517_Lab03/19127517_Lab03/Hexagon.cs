using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
namespace _19127517_Lab03
{
    class Hexagon : Polygon
    {
        public Hexagon()
        {
            nPoly = 6;// lục giác 6 cạnh
            init();
        }
        public override void set(Point pi, Point pj)
        {
            base.set(pi, pj);
            p2r.Y = p2.Y = p1.Y + (int)Math.Round((p2.X - p1.X) * Math.Sqrt(3) / 2.0);// tọa độ khi chiếu lên trục y góc 60 độ
            int rx = Math.Abs(p1.X - p2.X) / 2; // độ dài 1 cạnh của lục giác chiếu lên trục x
            int ry = Math.Abs(p1.Y - p2.Y) / 2; // độ dài 1 cạnh sau khi chiếu lên trục y góc 60 độ
            int dx = rx / 2;// 1/2 cạnh lục giác

            // Bắt đầu từ điểm trên bên trái đi theo chiều kim đồng hồ
            nPoints[0].setPoint(-dx, ry);
            nPoints[1].setPoint(dx, ry);
            nPoints[2].setPoint(rx, 0);
            nPoints[3].setPoint(dx, -ry);
            nPoints[4].setPoint(-dx, -ry);
            nPoints[5].setPoint(-rx, 0);

        }
    }
}
