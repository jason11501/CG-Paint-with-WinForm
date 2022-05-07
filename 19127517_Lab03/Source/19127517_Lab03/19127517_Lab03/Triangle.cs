using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
namespace _19127517_Lab03
{
    class Triangle : Polygon
    {
        public Triangle()
        {
            nPoly = 3;// tam giác đều
            init();
        }

        public override void set(Point A, Point B)
        {
            base.set(A, B);

            // tọa độ khi chiếu lên trục y góc 60 độ
            p2r.Y = p2.Y = p1.Y + (int)Math.Round((p2.X - p1.X) * Math.Sqrt(3) / 2.0);

            int rx = Math.Abs(p1.X - p2.X) / 2;// độ dài đáy
            int ry = Math.Abs(p1.Y - p2.Y) / 2;// chiều cao

            //Tọa độ các đỉnh     
            // Bắt đầu vẽ từ đỉnh ngược chiều kim đồng hồ       
            nPoints[0].setPoint(0, ry);
            nPoints[1].setPoint(-rx, -ry);
            nPoints[2].setPoint(rx, -ry);
        }
    }
}
