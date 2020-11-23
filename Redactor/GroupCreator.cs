using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redactor
{
    class GroupCreator : ITools
    {
        private Figure proto;

        public Figure Create(float x, float y)
        {
            Figure figure;
            figure = proto.Clone();
            figure.Resize(0, 0);
            figure.Move(x - figure.x - (figure.w / 2), y - figure.y - (figure.h / 2));
            return figure;
        }
        public Figure Create(float x, float y, float w, float h)
        {
            Figure figure;
            figure = proto.Clone();
            figure.Resize(figure.w-w,figure.h-h);
            figure.Move(x - figure.x - (figure.w / 2), y - figure.y - (figure.h / 2));
            return figure;
        }
        public void CopyProto(Group gp)
        {
            proto = gp.Clone();
        }
    }
}
