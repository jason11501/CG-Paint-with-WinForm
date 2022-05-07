using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
namespace _19127517_Lab03
{
    class Shape : Object
    {
        //Góc xoay hình
        public double Angle { get; set; } = 0.0;

        public override void set(Point pi, Point pj)
        {
            p1.setPoint(pi.X < pj.X ? pi.X : pj.X, pi.Y < pj.Y ? pi.Y : pj.Y);
            p2.setPoint(pi.X > pj.X ? pi.X : pj.X, pi.Y > pj.Y ? pi.Y : pj.Y);
        }
        //Xác định hình có được chọn không
        public override bool isInsideBox(int x, int y)
        {
            // Tính tâm
            int cx = (p1.X + p2.X) / 2, cy = (p1.Y + p2.Y) / 2;
            // phải trừ đi tâm vì trước khi vẽ đều tịnh tiến 1 đoạn cx,cy
            x -= cx;
            y -= cy;
            // tính 
            int u = (int)Math.Round(x * Math.Cos(-Angle * Math.PI / 180) - y * Math.Sin(-Angle * Math.PI / 180)),
                v = (int)Math.Round(y * Math.Cos(-Angle * Math.PI / 180) + x * Math.Sin(-Angle * Math.PI / 180));
            x = u; y = v;
            return !(x < p1.X - cx || x > p2.X - cx || y < p1.Y - cy || y > p2.Y - cy);
        }
        // Lấy các điểm điều khiển
        public override int getControlPointId(int x, int y)
        {
            // Do tịnh tiến hình nên tâm hình chính là gốc tọa độ
            int cx = 0, cy = 0;
            if (Angle != 0)
            {
                cx = (p1.X + p2.X) / 2; cy = (p1.Y + p2.Y) / 2;
                x -= cx;
                y -= cy;
                int u = (int)Math.Round(x * Math.Cos(-Angle * Math.PI / 180) - y * Math.Sin(-Angle * Math.PI / 180)),
                    v = (int)Math.Round(y * Math.Cos(-Angle * Math.PI / 180) + x * Math.Sin(-Angle * Math.PI / 180));
                x = u; y = v;
            }
            // tính tọa độ 2 điểm chặn của control box với epsilon=5
            int x1 = p1.X - cx - 5, y1 = p1.Y - cy - 5, x2 = p2.X - cx + 5, y2 = p2.Y - cy + 5;
            int xm = cx == 0 ? (p1.X + p2.X) / 2 : 0,
                ym = cy == 0 ? (p1.Y + p2.Y) / 2 : 0;
            /*Thứ tự Control Points
            *   8 
            2   7   1
            4       5
            0   6   3
            */

            //Tọa độ các điểm điều khiển
            int[][] cp = new int[][] {
                new int[]{x1, y1},
                new int[]{x2, y2},
                new int[]{x1, y2},
                new int[]{x2, y1},
                new int[]{x1, ym},
                new int[]{x2, ym},
                new int[]{xm, y1},
                new int[]{xm, y2},
                new int[]{xm, y2 + 20}
            };

            for (int i = 0; i < 9; i++)
            {
                if (x >= cp[i][0] - 5 && x <= cp[i][0] + 5 && y >= cp[i][1] - 5 && y <= cp[i][1] + 5)
                    return i;    //Điểm điều khiển đang được chọn           
            }
            return -1;
        }

        public override void dragControlPoint(int CPid, Point s, Point e)
        {
            //Vector tịnh tiến điểm điều khiển
            int vx = e.X - s.X,
                vy = e.Y - s.Y;

            if (CPid == 8)
            {
                int cx = (p1.X + p2.X) / 2, cy = (p1.Y + p2.Y) / 2;
                Point v1 = new Point(s.X - cx, s.Y - cy), v2 = new Point(e.X - cx, e.Y - cy);
                double alpha = Math.Acos((v1.X * v2.X + v1.Y * v2.Y) / (Math.Sqrt(v1.X * v1.X + v1.Y * v1.Y) * Math.Sqrt(v2.X * v2.X + v2.Y * v2.Y))) / Math.PI * 180.0;
                if (v1.X * v2.Y - v2.X * v1.Y < 0) alpha *= -1;
                Angle = alpha;
                return;
            }

            //Chiều cao và rộng của hình
            int h = p2.Y - p1.Y,
                w = p2.X - p1.X;
            //Tọa độ 2 điểm chặn
            int x1 = p1.X, y1 = p1.Y, x2 = p2.X, y2 = p2.Y;
            Point pu = new Point(), pv = new Point();
            pu.setPoint(p1); pv.setPoint(p2);

            /*Thứ tự Control Points
            *   8 
            2   7   1
            4       5
            0   6   3
            */

            if (CPid == 1 || CPid == 5 || CPid == 3)
            {
                if (-vx >= w)
                    vx = 1 - w;
            }
            else if (CPid == 2 || CPid == 4 || CPid == 0)
            {
                if (vx >= w)
                    vx = w - 1;
            }

            if (CPid == 2 || CPid == 7 || CPid == 1)
            {
                if (-vy >= h)
                    vy = 1 - h;
            }
            else if (CPid == 0 || CPid == 6 || CPid == 3)
            {
                if (vy >= h)
                    vy = h - 1;
            }

            //Co giãn các điểm điều khiển
            switch (CPid)
            {
                case 0:
                    pu.setPoint(x1 + vx, y1 + vy);
                    break;
                case 1:
                    pv.setPoint(x2 + vx, y2 + vy);
                    break;
                case 2:
                    pu.setPoint(x1 + vx, y1);
                    pv.setPoint(x2, y2 + vy);
                    break;
                case 3:
                    pu.setPoint(x1, y1 + vy);
                    pv.setPoint(x2 + vx, y2);
                    break;
                case 4:
                    pu.setPoint(x1 + vx, y1);
                    break;
                case 5:
                    pv.setPoint(x2 + vx, y2);
                    break;
                case 6:
                    pu.setPoint(x1, y1 + vy);
                    break;
                case 7:
                    pv.setPoint(x2, y2 + vy);
                    break;
            }

            p1.setPoint(pu);
            p2.setPoint(pv);
        }

        public override void drawControlBox(OpenGL gl)
        {
            int cx = (p1.X + p2.X) / 2,
                cy = (p1.Y + p2.Y) / 2;
            gl.Color(LineColor.R, LineColor.G, LineColor.B);
            gl.PushMatrix();
            gl.Translate(cx, cy, 0.0);
            gl.Rotate(Angle, 0.0, 0.0, 1.0);

            Rectangle box = new Rectangle();
            int x1 = p1.X - cx - 5, y1 = p1.Y - cy - 10, x2 = p2.X - cx + 5, y2 = p2.Y - cy + 10;
            Point pi = new Point(x1, y1),
                pj = new Point(x2, y2);
            box.LineColor = new Color(0.5f, 0.5f, 0.5f);

            //Vẽ khung bao quanh hình
            box.set(pi, pj);
            box.Draw(gl);

            //Vẽ 8 điểm điều khiển
            int xm = 0, //p1.X + p2.X) / 2,
               ym = 0;// (p1.Y + p2.Y) / 2;
            int[][] cp = new int[][] {
                new int[]{x1, y1},
                new int[]{x2, y2},
                new int[]{x1, y2},
                new int[]{x2, y1},
                new int[]{x1, ym},
                new int[]{x2, ym},
                new int[]{xm, y1},
                new int[]{xm, y2},
            };
            foreach (int[] p in cp)
            {
                box.set(new Point(p[0] - 3, p[1] - 3), new Point(p[0] + 3, p[1] + 3));
                box.Draw(gl);
            }

            //Vẽ điểm điều khiển xoay
            Line li = new Line();
            li.set(new Point(xm, y2), new Point(xm, y2 + 20 - 4));
            li.LineColor = new Color(0.5f, 0.5f, 0.5f);
            li.Draw(gl);

            Circle ci = new Circle();
            ci.set(new Point(xm - 3, y2 + 20 - 3), new Point(xm + 3, y2 + 20 + 3));
            ci.LineColor = new Color(0.5f, 0.5f, 0.5f);
            ci.Draw(gl);
            gl.PopMatrix();
        }

        public override void translate(Point s, Point e)
        {
            //Vector tịnh tiến
            int vx = e.X - s.X,
                vy = e.Y - s.Y;

            p1.X += vx;
            p1.Y += vy;
            p2.X += vx;
            p2.Y += vy;
        }

        private Color getPixelColor(OpenGL gl, int x, int y) //Lấy màu của pixel (x,y)
        {
            Color color = new Color();
            byte[] pixels = new byte[4];
            gl.ReadPixels(x, y, 2, 2, OpenGL.GL_RGB, OpenGL.GL_UNSIGNED_BYTE, pixels);
            color.setColor(pixels[0] / 255.0f, pixels[1] / 255.0f, pixels[2] / 255.0f);
            return color;
        }

        private void setPixelColor(OpenGL gl, int x, int y, Color color) //Set màu cho pixel (x,y)
        {
            gl.Color(color.R, color.G, color.B);
            gl.Begin(OpenGL.GL_POINTS);
            gl.Vertex(x, y);
            gl.End();
            gl.Flush();
        }

        private void floodFill(OpenGL gl, int x, int y, Color oldColor)
        {
            Color color = new Color(getPixelColor(gl, x, y));
            if (color == oldColor)
            {
                setPixelColor(gl, x, y, FillColor);
                floodFill(gl, x + 1, y, oldColor);
                floodFill(gl, x - 1, y, oldColor);
                floodFill(gl, x, y + 1, oldColor);
                floodFill(gl, x, y - 1, oldColor);
            }
        }

        public override void Fill(OpenGL gl, bool mode)
        {
            if (mode == true)
            {
                int cx = (p1.X + p2.X) / 2,
                    cy = (p1.Y + p2.Y) / 2;
                Color oldColor = getPixelColor(gl, cx, cy);
                floodFill(gl, cx, cy, oldColor);
            }
        }
    }
}
