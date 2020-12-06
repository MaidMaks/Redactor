using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Redactor
{
    class Manipulator : Figure
    {
        public Figure fig { get; private set; }

        private delegate void Corner(float dx, float dy);
        Corner corner;

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
                corner = figure;
                return true;
            }
            else if (Math.Abs(xx - x) <= 4 && Math.Abs(xx - x) >= 0 && Math.Abs(yy - y) <= 4 && Math.Abs(yy - y) >= 0)
            {
                corner = tLeft;
                return true;
            }
            else if (Math.Abs(xx - x) <= 4 && Math.Abs(xx - x) >= 0 && Math.Abs(yy - y - h) <= 4 && Math.Abs(yy - y - h) >= 0)
            {
                corner = bLeft;
                return true;
            }
            else if (Math.Abs(xx - x - w) <= 4 && Math.Abs(xx - x- w) >= 0 && Math.Abs(yy - y) <= 4 && Math.Abs(yy - y ) >= 0)
            {
                corner = tRight;
                return true;
            }
            else if (Math.Abs(xx - x - w) <= 4 && Math.Abs(xx - x - w) >= 0 && Math.Abs(yy - y - h) <= 4 && Math.Abs(yy - y - h) >= 0)
            {
                corner = bRight;
                return true;
            }
            return false;
        }

        private void bLeft(float dx, float dy)
        {
            fig.Move(dx, 0);
            fig.Resize(-dx, dy);
        }
        
        private void tLeft(float dx, float dy)
        {
            fig.Move(dx, dy);
            fig.Resize(-dx, -dy);
        }
        
        private void bRight(float dx, float dy)
        { 
            fig.Resize(dx, dy);
        }
        
        private void tRight(float dx, float dy)
        {
            fig.Move(0, dy);
            fig.Resize(dx, -dy);
        }

        private void figure(float dx, float dy)
        { 
            fig.Move(dx, dy); 
        }

        public void Drag(float dx, float dy)
        {
            corner(dx, dy);
            Update();
        }

        public override Figure Clone()
        {
            return fig;
        }
    }
}
