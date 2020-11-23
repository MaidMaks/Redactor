using System;
using System.Drawing;

namespace Redactor
{
    public abstract class Figure
    {
        public float x { get; protected set; }
        public float y { get; protected set; }
        public float w { get; protected set; }
        public float h { get; protected set; }


        public Figure(float x, float y, float w, float h)
        {
            this.x = x - w / 2;
            this.y = y - h / 2;
            this.w = w;
            this.h = h;
        }
        public abstract void Draw(Graphics gr);
        public abstract bool Touch(float x, float y);
        public virtual void Resize(float dw, float dh)
        {
            w += dw;
            h += dh;
        }
        public virtual void Move(float dx, float dy)
        {
            x += dx;
            y += dy;
        }
        public abstract Figure Clone();
    }

    public class Rect : Figure
    {
        private Rect(float x, float y, float w, float h) : base(x, y, w, h)
        { }
        public override void Draw(Graphics gr)
        {
            gr.FillRectangle(Brushes.MediumPurple, Convert.ToInt32(x), Convert.ToInt32(y), Convert.ToInt32(w), Convert.ToInt32(h));
        }
        public override bool Touch(float x, float y) => x < this.x + w && x > this.x && y > this.y && y < this.y + h;
        public override Figure Clone()
        {
            return new Rect(x, y, w, h);
        }
        public class RectCreator : ITools
        {
            public Figure Create(float x, float y)
            {
                return new Rect(x, y, 25, 25);
            }
            public Figure Create(float x, float y, float w, float h)
            {
                return new Rect(x, y, w, h);
            }
        }
    }

    public class Elips : Figure
    {
        private Elips(float x, float y, float w, float h) : base(x, y, w, h)
        { }
        public override void Draw(Graphics gr)
        {
            gr.FillEllipse(Brushes.MediumPurple, Convert.ToInt32(x), Convert.ToInt32(y), Convert.ToInt32(w), Convert.ToInt32(h));
        }
        public override bool Touch(float x, float y) => x < this.x + w && x > this.x && y > this.y && y < this.y + h;
        public override Figure Clone()
        {
            return new Elips(x, y, w, h);
        }
        public class ElipsCreator : ITools
        {
            public Figure Create(float x, float y)
            {
                return new Elips(x, y, 25, 25);
            }
            public Figure Create(float x, float y, float w, float h)
            {
                return new Elips(x, y, w, h);
            }
        }
    }
}