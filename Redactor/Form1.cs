using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Redactor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        enum CtrlStates
        { 
        Pressed,
        NotPressed
        }
        //CtrlStates ctrlStates = CtrlStates.NotPressed; //Отслеживание нажатой кнопки CTRL

        List<Figure> figures = new List<Figure>();
        List<ITools> tools = new List<ITools>();
        ITools tl;
        ISelect select;
        Point p;
        Manipulator mainManipul = new Manipulator(0, 0, 0, 0);
        Group groupOfFigures = new Group(0, 0, 0, 0);
        Figure fig;
        public Graphics gr;

        delegate void formTools(float x, float y); //Создаём делегат чтобы заменить if в picturebox_MouseDown
        formTools toolsJob; //Создаём переменную делегата чтобы заносить в неё методы для отработки

        private void Form1_Load(object sender, EventArgs e)
        {
            gr = pictureBox1.CreateGraphics();
            tl = null;
            tools.Add(null);
            tools.Add(new Rect.RectCreator());
            tools.Add(new Elips.ElipsCreator());
            toolsJob = selectFigure;
            select = new FigureSelect();
        }

        private void Move_Click(object sender, EventArgs e)
        {
            tl = null;
            toolsJob = selectFigure;
        }

        private void Rect_Click(object sender, EventArgs e)
        {
            tl = tools[1];
            toolsJob = paintFigure;
        }

        private void Circle_Click(object sender, EventArgs e)
        {
            tl = tools[2];
            toolsJob = paintFigure;
        }

        private void createGroupToolStripMenuItem_Click(object sender, EventArgs e) //Добавление элемента в туллбокс
        {
            if (mainManipul.fig == null)
                return;
            GroupCreator groupCreator = new GroupCreator();
            groupCreator.CopyProto(groupOfFigures);
            tools.Add(groupCreator);                                //Добавление Creator'a нашей группы в инструменты(tools)
            toolStripComboBox1.Items.Add("Group " + (toolStripComboBox1.Items.Count + 1));
        }

        private void DrawGroup_Click(object sender, EventArgs e) //Выбираем группу
        {
            if (toolStripComboBox1.SelectedIndex == -1)
                return;
            tl = tools[toolStripComboBox1.SelectedIndex + 3];
            toolsJob = paintFigure;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Figure figs in figures)
                figs.Draw(e.Graphics);
            mainManipul.Draw(e.Graphics);
        }

        private void paintFigure(float x, float y)
        {
            figures.Add(tl.Create(x, y));
            fig = figures.Last<Figure>();
            fig.Draw(gr);
        }

        private void selectFigure(float x, float y)
        {
            //else
            //    switch (ctrlStates)
            //    {
            //        case CtrlStates.NotPressed:
            //            foreach (var fig in figures)
            //                if (fig.Touch(x, y))
            //                {
            //                    groupOfFigures.Clear();
            //                    groupOfFigures.Add(fig);
            //                    mainManipul.Attach(fig);
            //                    break;
            //                }
            //            break;
            //        case CtrlStates.Pressed:
            //            foreach (var fig in figures)
            //                if (fig.Touch(x, y))
            //                {
            //                    groupOfFigures.Add(fig);
            //                    mainManipul.Attach(groupOfFigures);
            //                    mainManipul.Update();
            //                }
            //            break;
            //    }
            if (mainManipul.Touch(x, y)) return;
            else if (groupOfFigures.Touch(x, y)) return;
            select.select(figures, groupOfFigures, mainManipul, x, y);
            p = new Point((int)x, (int)y);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            toolsJob(e.X, e.Y);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                if (mainManipul.fig != null)
                {
                    mainManipul.Drag(e.X - p.X, e.Y - p.Y);
                }
            p = e.Location;
            pictureBox1.Refresh();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            select = new GroupSelect();
            //if (e.Control)
            //    ctrlStates = CtrlStates.Pressed;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            select = new FigureSelect();
            //if (!e.Control)
            //    ctrlStates = CtrlStates.NotPressed;
        }
    }
}
