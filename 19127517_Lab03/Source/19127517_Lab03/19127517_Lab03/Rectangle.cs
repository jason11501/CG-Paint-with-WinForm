using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
namespace _19127517_Lab03
{
    class Rectangle : Polygon
    {
        public Rectangle()
        {
            nPoly = 4;// hình chữ nhật
            init();
        }

        public override void set(Point A, Point B)
        {
            base.set(A, B);
            int rx = Math.Abs(p1.X - p2.X) / 2;// tính chiều dài
            int ry = Math.Abs(p1.Y - p2.Y) / 2;// tính chiều rộng

            //Tọa độ các đỉnh
            // Bắt đầu vẽ từ góc dưới bên trái ngược chiều kim đồng hồ
            nPoints[0].setPoint(-rx, -ry);
            nPoints[1].setPoint(rx, -ry);
            nPoints[2].setPoint(rx, ry);
            nPoints[3].setPoint(-rx, ry);
        }
    }
}
