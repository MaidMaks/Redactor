using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Redactor
{
    class Manipulator : Figure
    {
        public Figure fig { get; private set; }
        enum Corners
        {
            Bl,
            Br,
            Tl,
            Tr,
            Figure,
            None
        }
        Corners corn = Corners.None;

        public Manipulator(float x, float y, float w, float h) : base(x, y, w, h)
        {

        }

        public void Attach(Figure figureToAdd) => fig = figureToAdd;

        public void Clear() => fig = null;

        public void Update()
        {
            x = fig.x;
            y = fig.y;
            w = fig.w;
            h = fig.h;
        }

        public override void Draw(Graphics gr)
        {
            Pen p = new Pen(Color.Black, 1);
            p.DashStyle = DashStyle.Dash;
            p.DashPattern = new float[2] { 7, 3 };
            Pen p1 = new Pen(Color.Black, 1);
            gr.DrawRectangle(p, x - 2, y - 2, w + 4, h + 4);
            gr.DrawRectangle(p1, x - 4, y - 4, 4, 4);
            gr.DrawRectangle(p1, x + w, y - 4, 4, 4);
            gr.DrawRectangle(p1, x - 4, y + h, 4, 4);
            gr.DrawRectangle(p1, x + w, y + h, 4, 4);
        }

        public override bool Touch(float xx, float yy)
        {
            if (fig == null) return false;

            if (fig.Touch(xx, yy))
            {
                corn = Corners.Figure;
                return true;
            }
            else if (Math.Abs(xx - x) <= 4 && Math.Abs(xx - x) >= 0 && Math.Abs(yy - y) <= 4 && Math.Abs(yy - y) >= 0)
            {
                corn = Corners.Tl;
                return true;
            }
            else if (Math.Abs(xx - x) <= 4 && Math.Abs(xx - x) >= 0 && Math.Abs(yy - y - h) <= 4 && Math.Abs(yy - y - h) >= 0)
            {
                corn = Corners.Bl;
                return true;
            }
            else if (Math.Abs(xx - x - w) <= 4 && Math.Abs(xx - x- w) >= 0 && Math.Abs(yy - y) <= 4 && Math.Abs(yy - y ) >= 0)
            {
                corn = Corners.Tr;
                return true;
            }
            else if (Math.Abs(xx - x - w) <= 4 && Math.Abs(xx - x - w) >= 0 && Math.Abs(yy - y - h) <= 4 && Math.Abs(yy - y - h) >= 0)
            {
                corn = Corners.Br;
                return true;
            }
            return false;
        }

        public void Drag(float dx, float dy)
        {
            switch (corn)
            {
                case Corners.Tl:
                    fig.Move(dx, dy);
                    fig.Resize(-dx, -dy);
                    break;
                case Corners.Tr:
                    fig.Move(0, dy);
                    fig.Resize(dx, -dy);
                    break;
                case Corners.Bl:
                    fig.Move(dx, 0);
                    fig.Resize(-dx, dy);
                    break;
                case Corners.Br:
                    
                    fig.Resize(dx, dy);
                    break;
                case Corners.Figure:
                    fig.Move(dx, dy);
                    break;
                default:
                    break;
            }
            Update();
        }
        public override Figure Clone()
        {
            return fig;
        }
    }
}
