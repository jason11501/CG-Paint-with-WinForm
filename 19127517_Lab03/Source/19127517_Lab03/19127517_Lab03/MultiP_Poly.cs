using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
namespace _19127517_Lab03
{
    class MultiP_Poly : Polygon
    {
        // List các điểm của đa giác
        List<Point> vertexes = new List<Point>();

        // Thêm đỉnh vào list
        public void addVertex(Point p)
        {
            vertexes.Add(p);
            nPoly++;
        }

        public void moveVertex(Point p)
        {
            if (nPoly > 0)
                vertexes[nPoly - 1].setPoint(p);
        }

        public override void Draw(OpenGL gl)
        {
            if (nPoly <= 1) return;
            Line li = new Line();
            li.LineColor = LineColor;
            li.LineWidth = LineWidth;

            //Vẽ từng cạnh bằng các nối lần lược các đỉnh
            Point start = new Point(), end = new Point();
            start.setPoint(vertexes[nPoly - 1]);
            for (int i = 0; i < nPoly; i++)
            {
                end.setPoint(vertexes[i]);
                li.set(start, end);
                li.Draw(gl);
                start.setPoint(end);
            }
        }

        public Polygon getPolygon()
        {
            nPoly--;
            if (nPoly < 2) return null;

            Polygon p = new Polygon();
            p.nPoly = nPoly;
            p.nPoints = new Point[nPoly];
            p.LineColor = LineColor;
            p.LineWidth = LineWidth;
            p.FillColor = FillColor;

            // tìm xmin, ymin, xmax, ymax để vẽ khung
            int x1 = vertexes[0].X, x2 = vertexes[0].X, y1 = vertexes[0].Y, y2 = vertexes[0].Y;
            for (int i = 1; i < nPoly; i++)
            {
                if (vertexes[i].X < x1) x1 = vertexes[i].X;
                if (vertexes[i].X > x2) x2 = vertexes[i].X;

                if (vertexes[i].Y < y1) y1 = vertexes[i].Y;
                if (vertexes[i].Y > y2) y2 = vertexes[i].Y;
            }
            p.set(new Point(x1, y1), new Point(x2, y2));
            int xc = (x1 + x2) / 2,
                yc = (y1 + y2) / 2;
            for (int i = 0; i < nPoly; i++)
            {
                p.nPoints[i] = new Point(vertexes[i].X - xc, vertexes[i].Y - yc);
            }

            return p;
        }
    }
}
