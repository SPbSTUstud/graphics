using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace polar
{
    public partial class Form1 : Form
    {
        private float _radius;
        private float _angle;

        private Point _decartCenter;
        private Point _polarCenter;

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            Draw2dDecart(e.Graphics, new Point(100, 150), 80);
            Draw2dPolar(e.Graphics, new Point(300, 150), 80);

            Draw2dPointInDecart(e.Graphics, CoorHelper.ToDecart(_radius, _angle));
            Draw2dPointInPolar(e.Graphics, _radius, _angle);
        }

        public void Draw2dDecart(Graphics g, Point center, int size)
        {
            Pen p = new Pen(Brushes.DeepSkyBlue);
            _decartCenter = center;
            g.DrawLine(p, center.X - size, center.Y, center.X + size, center.Y);
            for(int i=center.X - size; i<center.X + size; i+=10)
                g.DrawLine(p, i, center.Y - 1, i, center.Y + 1);
            g.DrawLine(p, center.X, center.Y - size, center.X, center.Y + size);
            for (int i = center.Y - size; i < center.Y + size; i+=10)
                g.DrawLine(p, center.X - 1, i, center.X + 1, i);
        }

        public void Draw2dPolar(Graphics g, Point center, int size)
        {
            Pen p = new Pen(Brushes.PowderBlue);
            _polarCenter = center;
            g.DrawLine(p, center.X - size, center.Y, center.X + size, center.Y);
        }

        public void Draw2dPointInDecart(Graphics g, PointF p)
        {
            Pen pen = new Pen(Brushes.DarkRed);
            g.DrawRectangle(pen, _decartCenter.X + p.X - 1, _decartCenter.Y - p.Y - 1, 2f, 2f);
        }

        public void Draw2dPointInPolar(Graphics g, float radius, float angle)
        {
            Pen pen = new Pen(Brushes.DarkOrange);
            PointF p = CoorHelper.ToDecart((float)radius, (float)angle);
            g.DrawLine(pen, _polarCenter, new Point(_polarCenter.X + (int)p.X, _polarCenter.Y - (int)p.Y));
            g.DrawRectangle(pen, _polarCenter.X + p.X - 1, _polarCenter.Y - p.Y - 1, 2f, 2f);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!float.TryParse(textBox1.Text, out _radius))
                MessageBox.Show("Некорректное значение радиуса");
            PointF p = CoorHelper.ToDecart(_radius, _angle);
            textBox3.Text = p.X.ToString();
            textBox4.Text = p.Y.ToString();
            Invalidate();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!float.TryParse(textBox2.Text, out _angle))
                MessageBox.Show("Некорректное значение угла");
            PointF p = CoorHelper.ToDecart(_radius, _angle);
            textBox3.Text = p.X.ToString();
            textBox4.Text = p.Y.ToString();
            Invalidate();
        }
    }
}
