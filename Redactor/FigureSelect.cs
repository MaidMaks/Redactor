using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redactor
{
    interface ISelect
    {
        void select(List<Figure> figures, Group group, Manipulator manipulator, float x, float y);
    }

    class FigureSelect : ISelect
    {
        public void select(List<Figure> figures, Group group, Manipulator manipulator, float x, float y)
        {
            foreach (var fig in figures)
                if (fig.Touch(x, y))
                {
                    group.Clear();
                    group.Add(fig);
                    manipulator.Attach(fig);
                    break;
                }
        }
    }

    class GroupSelect : ISelect
    {
        public void select(List<Figure> figures, Group group, Manipulator manipulator , float x, float y)
        {
            foreach (var fig in figures)
                if (fig.Touch(x, y))
                {
                    group.Add(fig);
                    manipulator.Attach(group);
                    manipulator.Update();
                }
        }
    }
}
