using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
namespace _19127517_Lab03
{
    class Polygon : Shape
    {
        public int nPoly = 0;// số cạnh đa giác
        public Point[] nPoints;// tập hợp tọa độ các điểm
        protected Point p1r = new Point(), p2r = new Point();// 2 điểm lưu tọa độ trước khi scale
        // khởi tạo các điểm của đa giác
        protected void init()
        {
            nPoints = new Point[nPoly];
            for (int i = 0; i < nPoly; i++)
                nPoints[i] = new Point();
        }
        // Set tọa độ 2 điểm chặn và lưu tọa độ trước khi scale
        public override void set(Point A, Point B)
        {
            base.set(A, B);
            p1r.setPoint(p1);
            p2r.setPoint(p2);
        }

        // Vẽ đa giác
        public override void Draw(OpenGL gl)
        {
            // Khởi tạo nét vẽ, màu nét vẽ, độ dày nét vẽ
            Line li = new Line();
            li.LineWidth = LineWidth;
            li.LineColor = LineColor;

            // Tính tọa độ tâm của hình
            int cx = (p1.X + p2.X) / 2, cy = (p1.Y + p2.Y) / 2;

            // Đẩy ma trận hiện hành vào stack
            gl.PushMatrix();
            // Tịnh tiến về tâm
            gl.Translate(cx, cy, 0.0);
            // Xoay 1 góc angle
            gl.Rotate(Angle, 0.0, 0.0, 1.0);
            // Scale hình
            gl.Scale((double)(p2.X - p1.X) / (p2r.X - p1r.X), (double)(p2.Y - p1.Y) / (p2r.Y - p1r.Y), 0.0);

            //Vẽ từng cạnh bằng các nối lần lược các đỉnh
            Point start = new Point(), end = new Point();

            // Khởi tạo điểm bắt đầu là điểm cuối cùng của đa giác
            start.setPoint(nPoints[nPoly - 1]);
            for (int i = 0; i < nPoly; i++)
            {
                end.setPoint(nPoints[i]);
                li.set(start, end);
                li.Draw(gl);// vẽ cạnh
                start.setPoint(end);// set điểm kế tiếp
            }
            // Lấy ma trận hiện hành ra khởi stack
            gl.PopMatrix();
        }
        public override void Fill(OpenGL gl, bool mode)
        {
            if (mode)
            {
                base.Fill(gl, mode);
            }
            else //scanline
            {
                if (nPoly < 3) return;
                ScanFill fillPolygon = new ScanFill();
                // List chứa các cạnh của đa giác
                List<Point> p = new List<Point>();
                // Add từng cạnh của đa giác vào list trên
                for (int i = 0; i < nPoly; i++)
                {
                    p.Add(nPoints[i]);
                }
                // Tính tâm của hình
                int cx = (p1.X + p2.X) / 2, cy = (p1.Y + p2.Y) / 2;

                // Đẩy ma trận hiện hành vào stack
                gl.PushMatrix();
                // Tịnh tiền về tâm
                gl.Translate(cx, cy, 0.0);
                // Xoay một góc Angle
                gl.Rotate(Angle, 0.0, 0.0, 1.0);
                // Scale hình
                gl.Scale((double)(p2.X - p1.X) / (p2r.X - p1r.X), (double)(p2.Y - p1.Y) / (p2r.Y - p1r.Y), 0.0);
                // Set màu tô bằng màu của fillcolor của hình đang vẽ
                gl.Color(FillColor.getR(), FillColor.getG(), FillColor.getB());
                // Set các giá trị ban đầu cho lớp scanfill
                fillPolygon.setFill(p);
                // Khởi tạo các cạnh
                fillPolygon.initEdges();
                // Thực hiện tô màu
                fillPolygon.scanlineFill(gl);
                // Đẩy ma trận hiện hành ra khỏi stack
                gl.PopMatrix();
            }
        }

    }
}
