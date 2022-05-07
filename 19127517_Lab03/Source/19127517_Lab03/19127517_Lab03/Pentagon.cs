using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
namespace _19127517_Lab03
{
    class Pentagon : Polygon
    {
        public Pentagon()
        {
            nPoly = 5;// ngũ giác
            init();
        }

        public override void set(Point A, Point B)
        {

            //Point Min = new Point(A.X < B.X ? A.X : B.X, A.Y < B.Y ? A.Y : B.Y),
            //      Max1 = new Point(A.X > B.X ? A.X : B.X, A.Y > B.Y ? A.Y : B.Y);
            //int d = Max1.X - Min.X;
            //Point Max = new Point(Max1.X, Min.Y + d);


            ////Min chính là A bên dưới, Max chính là B bên dưới
            ///* 
            // *   C - 0 - B  
            // *   |/     \|
            // *   1       4
            // *   |\     /|
            // *   A-2 - 3-D
            // * 
            // */

            //nPoints[0].setPoint((Max.X + Min.X) / 2, Max.Y);
            ////Khoảng cách từ 0 -> C
            //int d0C = nPoints[0].X - Min.X;
            ////Khoảng cách từ C -> 1
            //int dC1 = (int)Math.Round(d0C / Math.Tan(54 * Math.PI / 180));
            //nPoints[1].setPoint(Min.X, Max.Y - dC1);
            //nPoints[4].setPoint(Max.X, Max.Y - dC1);
            ////Khoảng cách từ 1 -> A
            //int d1A = nPoints[1].Y - Min.Y;
            ////Khoảng cách từ A -> 2 = khoảng cách từ 3 -> D
            //int dA2 = (int)Math.Round(d1A / Math.Tan(72 * Math.PI / 180));
            //nPoints[2].setPoint(Min.X + dA2, Min.Y);
            //nPoints[3].setPoint(Max.X - dA2, Min.Y);
            base.set(A, B);
            p2r.Y = p2.Y = p1.Y + (p2.X - p1.X);// tọa độ 
            int rx = Math.Abs(p1.X - p2.X) / 2;
            int ry = Math.Abs(p1.Y - p2.Y) / 2;

            int x18 = (int)Math.Round(rx * Math.Cos(18 * Math.PI / 180)),
                x54 = (int)Math.Round(rx * Math.Cos(54 * Math.PI / 180)),
                y18 = (int)Math.Round(ry * Math.Sin(18 * Math.PI / 180)),
                y54 = (int)Math.Round(ry * Math.Sin(54 * Math.PI / 180));
            nPoints[0].setPoint(0, ry);
            nPoints[1].setPoint(x18, y18);
            nPoints[2].setPoint(x54, -y54);
            nPoints[3].setPoint(-x54, -y54);
            nPoints[4].setPoint(-x18, y18);
        }
    }
}
