using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Redactor
{
    class Group : Figure
    {
        List<Figure> figures = new List<Figure>();

        public Group(float x, float y, float w, float h) : base(x, y, w, h)
        {

        }

        public override void Draw(Graphics gr)
        {
            foreach (Figure fig in figures) { fig.Draw(gr); }
        }

        public override bool Touch(float x, float y) => x < this.x + w && x > this.x && y > this.y && y < this.y + h;

        public override void Move(float dx, float dy)
        {
            foreach (Figure fig in figures) { fig.Move(dx, dy); }
            x += dx;
            y += dy;
        }

        public override void Resize(float dw, float dh)
        {
            float kw = dw / w, kh = dh / h;
            foreach (Figure fig in figures)
            {
                fig.Resize(fig.w * kw, fig.h * kh);
                fig.Move(kw * (fig.x - x), kh * (fig.y - y));
            }
            w += dw;
            h += dh;
        }

        public void Add(Figure simpleFigure)
        {
            int count = 0;
            foreach (Figure fig in figures)
            {
                if (fig == simpleFigure)
                    count++;
            }
            if (count == 0)
            {
                figures.Add(simpleFigure);
            }
            Update();
        }

        public void Clear()
        {
            figures.Clear();
            x = 0;
            y = 0;
            h = 0;
            w = 0;
        }

        private void Update()
        {
            x = figures.Min(f => f.x);
            y = figures.Min(f => f.y);
            w = figures.Max(f => f.x + f.w) - x;
            h = figures.Max(f => f.y + f.h) - y;
        }
        public override Figure Clone()
        {
            Group gr = new Group(x, y, w, h);
            foreach (Figure a in figures)
                gr.Add(a.Clone());
            return gr;
        }
    }
}