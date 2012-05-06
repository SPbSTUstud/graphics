using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace circle
{
    public partial class Form1 : Form
    {
        private System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

        private int n;
        private int deltaX = 10;
        private int deltaY = 10;

        private int x;
        private int y;
        private int r;

        private long? time1;
        private long? time2;

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            DrawGrid(e.Graphics);

            DrawCircleSymmetry(e.Graphics, new Point(x, y), r);
            DrawCircleBresenhem(e.Graphics, new Point(x + 2 * r + 3, y), r);
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);

            int n1 = this.Height / deltaX;
            int n2 = this.Width / deltaY;
            
            n = n1 > n2 ? n1 : n2;
            
            Invalidate();
        }

        private void DrawGrid(Graphics e)
        {
            int n1 = this.Height / deltaX;
            int n2 = this.Width / deltaY;

            n = n1 > n2 ? n1 : n2;
            for (int i = 0; i < n + 1; i++)
                e.DrawLine(new Pen(Brushes.Black, 1), new Point(i * deltaY, 0), new Point(i * deltaY, this.Height));

            for (int i = 0; i < n + 1; i++)
                e.DrawLine(new Pen(Brushes.Black, 1), new Point(0, i * deltaX), new Point(this.Width, i * deltaX));
        }

        private void DrawCircleSymmetry(Graphics e, Point center, int radius)
        {
            stopwatch.Reset();
            stopwatch.Start();
            for (int x = radius; x >= 0; x--)
            {
                int y = (int)Math.Sqrt(radius * radius - x * x);
                e.FillRectangle(Brushes.Blue, (center.X + x) * deltaX, (center.Y + y) * deltaY, deltaX, deltaY);
                e.FillRectangle(Brushes.Blue, (center.X + x) * deltaX, (center.Y - y) * deltaY, deltaX, deltaY);
                e.FillRectangle(Brushes.Blue, (center.X - x) * deltaX, (center.Y + y) * deltaY, deltaX, deltaY);
                e.FillRectangle(Brushes.Blue, (center.X - x) * deltaX, (center.Y - y) * deltaY, deltaX, deltaY);
            }
            stopwatch.Stop();
            //if (!time1.HasValue)
            //{
            time1 = stopwatch.ElapsedTicks;
            Console.WriteLine("Iteration: {0} ticks", time1.Value);
            //}
        }

        private void DrawCircleBresenhem(Graphics e, Point center, int radius)
        {
            // R - радиус, X1, Y1 - координаты центра
           int x = 0;
           int y = radius;
           int delta = 3 - 2 * radius;
           int error = 0;
           stopwatch.Reset();
           stopwatch.Start();
           while (x <= y)
           {   
               e.FillRectangle(Brushes.Brown, (center.X + x) * deltaX, (center.Y + y) * deltaY, deltaX, deltaY);
               e.FillRectangle(Brushes.Brown, (center.X + x) * deltaX, (center.Y - y) * deltaY, deltaX, deltaY);
               e.FillRectangle(Brushes.Brown, (center.X - x) * deltaX, (center.Y + y) * deltaY, deltaX, deltaY);
               e.FillRectangle(Brushes.Brown, (center.X - x) * deltaX, (center.Y - y) * deltaY, deltaX, deltaY);
               e.FillRectangle(Brushes.Brown, (center.X + y) * deltaX, (center.Y + x) * deltaY, deltaX, deltaY);
               e.FillRectangle(Brushes.Brown, (center.X + y) * deltaX, (center.Y - x) * deltaY, deltaX, deltaY);
               e.FillRectangle(Brushes.Brown, (center.X - y) * deltaX, (center.Y + x) * deltaY, deltaX, deltaY);
               e.FillRectangle(Brushes.Brown, (center.X - y) * deltaX, (center.Y - x) * deltaY, deltaX, deltaY);
       
               if (delta < 0)
               {
                   delta += 4 * x++ + 6;
                   continue;
               }
               else
               {
                   delta += 4 * (x++ - --y) + 10;
                   continue;
               }
           }
           stopwatch.Stop();
           //if (!time2.HasValue)
           //{
           time2 = stopwatch.ElapsedTicks;
           Console.WriteLine("Bresenhem: {0} ticks", time2.Value);
            //}
        }

        private int Round(double d)
        {
            int i = (int)d;
            if (d - i >= 0.5)
                i++;
            return i;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int n1 = this.Height / deltaX;
            int n2 = this.Width / deltaY;

            n = n1 > n2 ? n1 : n2;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int xs = 0;
            if (!int.TryParse(textBox1.Text, out xs))
            {
                textBox1.Text = "0";
                MessageBox.Show("Некорректное значение");
            }
            if (xs < 0 || xs > 40)
            {
                textBox2.Text = "0";
                MessageBox.Show("Некорректное значение");
            }
            this.x = xs;

            Invalidate();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int ys = 0;
            if (!int.TryParse(textBox2.Text, out ys))
            {
                textBox2.Text = "0";
                MessageBox.Show("Некорректное значение");
            }
            if (ys < 0 || ys > 40)
            {
                textBox2.Text = "0";
                MessageBox.Show("Некорректное значение");
            }
            this.y = ys;

            Invalidate();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            int r = 0;
            if (!int.TryParse(textBox4.Text, out r))
            {
                textBox4.Text = "0";
                MessageBox.Show("Некорректное значение");
            }
            if (r > 10)
            {
                textBox4.Text = "0";
                MessageBox.Show("Некорректное значение");
            }
            this.r = r;
            Invalidate();
        }
    }
}

