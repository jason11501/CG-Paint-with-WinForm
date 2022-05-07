using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
namespace _19127517_Lab03
{
    class Object
    {
        // Màu nét vẽ
        public Color LineColor { get; set; } = new Color();
        // Màu tô
        public Color FillColor { get; set; } = new Color();
        // Góc xoay
        public double Angle { get; set; } = 0.0;
        // Độ dày viền
        public int LineWidth { get; set; } = 1;
        // Điểm chặn trên trái
        protected Point p1 = new Point();
        // Điểm chặn dưới phải
        protected Point p2 = new Point();
        // Set 2 điểm chặn
        public virtual void set(Point A, Point B)
        {
            p1.setPoint(Math.Min(A.X, B.X), Math.Min(A.Y, B.Y));
            p2.setPoint(Math.Max(A.X, B.X), Math.Max(A.X, B.X));
        }
        public virtual void Draw(OpenGL gl) { }
        //Xác định hình có được chọn không
        public virtual bool isInsideBox(int x, int y) { return false; }
        //Xác định điểm điều khiển được chọn
        public virtual int getControlPointId(int x, int y) { return -1; }
        //Tịnh tiến hình
        public virtual void translate(Point s, Point e) { }
        //Co giản hình qua điểm điều khiển
        public virtual void dragControlPoint(int CPid, Point s, Point e) { }
        //Vẽ các điểm điều khiển
        public virtual void drawControlBox(OpenGL gl) { }
        //Tô màu, nếu mode là true thì flood fill ngược lại là scanline fill
        public virtual void Fill(OpenGL gl, bool mode) { }
    }
}
