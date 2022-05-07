using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
namespace _19127517_Lab03
{
    class Line : Object
    {
        // các constructor 
        public Line() { }
        public Line(Point s, Point e)
        {
            p1.setPoint(s);
            p2.setPoint(e);
        }
        public override void set(Point A, Point B)
        {
            p1.setPoint(A.X, A.Y);
            p2.setPoint(B.X, B.Y);
        }

        // Lấy các đầu mút
        public Point getP1()
        {
            return p1;
        }
        public Point getP2()
        {
            return p2;
        }

        public override bool isInsideBox(int x, int y)
        {
            if (Math.Abs(y - p1.Y) <= 2.0 && Math.Abs(p2.Y - p1.Y) <= 2.0)
                return (x >= p1.X && x <= p2.X) || (x < p1.X && x > p2.X);
            if (Math.Abs(x - p1.X) <= 2.0 && Math.Abs(p2.X - p1.X) <= 2.0)
                return (y >= p1.Y && y <= p2.Y) || (y < p1.Y && y > p2.Y);

            double a = (double)(x - p1.X) / (y - p1.Y), b = (double)(p2.X - p1.X) / (p2.Y - p1.Y);
            return (Math.Abs(a - b) < 0.5);
        }

        public override void translate(Point s, Point e)
        {
            //Vector tịnh tiến
            int vx = e.X - s.X,
                vy = e.Y - s.Y;

            //Tịnh tiến các điểm điều khiển
            p1.X += vx;
            p2.X += vx;
            p1.Y += vy;
            p2.Y += vy;
        }

        public override int getControlPointId(int x, int y)
        {
            //Tọa độ các điểm điều khiển
            int[][] cp = new int[][] {
                new int[]{p1.X, p1.Y},
                 new int[]{ p2.X, p2.Y }
            };

            //Xác định điểm được chọn
            for (int i = 0; i < 2; i++)
            {
                if (x >= cp[i][0] - 5 && x <= cp[i][0] + 5 && y >= cp[i][1] - 5 && y <= cp[i][1] + 5)
                    return i;
            }
            return -1;
        }

        public override void dragControlPoint(int CPid, Point s, Point e)
        {
            //Vector tịnh tiến
            int vx = e.X - s.X,
                vy = e.Y - s.Y;

            int x1 = p1.X, y1 = p1.Y, x2 = p2.X, y2 = p2.Y;
            Point pu = new Point(), pv = new Point();
            pu.setPoint(p1); pv.setPoint(p2);

            //Co giãn hình
            if (CPid == 0)
                pu.setPoint(x1 + vx, y1 + vy);
            else
                pv.setPoint(x2 + vx, y2 + vy);

            p1.setPoint(pu);
            p2.setPoint(pv);
        }

        public override void drawControlBox(OpenGL gl)
        {
            Rectangle box = new Rectangle();
            Color c = new Color(0.5f, 0.5f, 0.5f);
            box.LineColor = c;

            //Tọa độ các điểm điều khiển
            int[][] cp = new int[][] {
                new int[]{p1.X, p1.Y},
                 new int[]{ p2.X, p2.Y }
            };

            //Vẽ các điểm điều khiển
            foreach (int[] p in cp)
            {
                box.set(new Point(p[0] - 3, p[1] - 3), new Point(p[0] + 3, p[1] + 3));
                box.Draw(gl);
                gl.Flush();
            }
        }

        public override void Draw(OpenGL gl)
        {
            // Gán màu vẽ bằng LineColor
            gl.Color(LineColor.R, LineColor.G, LineColor.B);
            // Gán độ dày nét vẽ bằng LineWidth
            gl.LineWidth(LineWidth);
            // Mở chế độ vẽ Line
            gl.Begin(OpenGL.GL_LINES);
            // 2 đầu mút
            gl.Vertex2sv(new short[] { (short)p1.X, (short)p1.Y });
            gl.Vertex2sv(new short[] { (short)p2.X, (short)p2.Y });
            //
            gl.End();
        }
    } 
}
