using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace rastr
{
    public partial class Form1 : Form
    {
        private System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

        private int n;
        private int deltaX;
        private int deltaY;

        private Point from = new Point(0, 1);
        private Point to = new Point(20, 38);

        private Point from2 = new Point(5, 1);
        private Point to2 = new Point(25, 38);

        long? time1;
        long? time2;

        public Form1()
        {
            InitializeComponent();
            textBox1.Text = from.X.ToString();
            textBox5.Text = from.Y.ToString();
            textBox2.Text = to.X.ToString();
            textBox3.Text = to.Y.ToString();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            DrawGrid(e.Graphics);

            //DrawLine(e.Graphics, from, to);
            Point p1 = from;
            Point p2 = to;
            
            double delta = (double)(p1.X - p2.X) / (p1.Y - p2.Y);
            DrawLineIteration(e.Graphics, p1, p2, delta);
            
            DrawLineBresenhem(e.Graphics, new Point(p1.X + 5, p1.Y), new Point(p2.X + 5, p2.Y));
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);

            n = 40;
            deltaX = this.Height / n;
            deltaY = this.Width / n;

            Invalidate();
        }

        private void DrawGrid(Graphics e)
        {
            int n = 40;
            int deltaX = this.Height / n;
            int deltaY = this.Width / n;
            for (int i = 0; i < n + 1; i++)
                e.DrawLine(new Pen(Brushes.Black, 1), new Point(i * deltaY, 0), new Point(i * deltaY, this.Height));

            for (int i = 0; i < n + 1; i++)
                e.DrawLine(new Pen(Brushes.Black, 1), new Point(0, i * deltaX), new Point(this.Width, i * deltaX));
        }

        private void DrawLine(Graphics e, Point from, Point to)
        {
            int k = (from.Y - to.Y) / (from.X - to.X);
            int b = from.Y - k * from.X;

            for (int i = from.X; i < to.X; i++)
                e.FillRectangle(Brushes.Blue, new Rectangle(new Point(i * deltaY, (k * i + b) * deltaX), new Size(deltaY, deltaX)));
        }

        private void DrawLineIteration(Graphics e, Point from, Point to, double delta)
        {
            if (double.IsInfinity(delta) || double.IsNaN(delta))
                return;

            int counter = 0;
            stopwatch.Reset();
            stopwatch.Start();

            if (Math.Abs(delta) >= 1)
            {
                //x++
                int x = from.X;
                int incr = 1;
                if (from.X > to.X)
                {
                    x = to.X;
                    incr = -1;
                }

                if (from.Y - to.Y < 0)
                    for (double i = from.Y; i <= to.Y; i += 1 / delta)
                    {
                        e.FillRectangle(Brushes.IndianRed, new Rectangle(new Point(x * deltaY, Trunc(i) * deltaX), new Size(deltaY, deltaX)));                    
                        x += incr;
                        counter++;
                    }   
                if (from.Y - to.Y > 0)
                    for (double i = from.Y; i >= to.Y; i += 1 / delta)
                    {
                        e.FillRectangle(Brushes.IndianRed, new Rectangle(new Point(x * deltaY, Trunc(i) * deltaX), new Size(deltaY, deltaX)));
                        x += incr;
                        counter++;
                    }
                e.FillRectangle(Brushes.IndianRed, new Rectangle(new Point(to.X * deltaY, to.Y * deltaX), new Size(deltaY, deltaX)));
                e.FillRectangle(Brushes.IndianRed, new Rectangle(new Point(from.X * deltaY, from.Y * deltaX), new Size(deltaY, deltaX)));
            }
            else
            {
                //y++
                int y = from.Y;
                int incr = 1;
                if (from.Y > to.Y)
                {
                    y = to.Y;
                    incr = -1;
                }
                if (from.X - to.X < 0)
                    for (double i = from.X; i <= to.X; i += delta)
                    {
                        e.FillRectangle(Brushes.IndianRed, new Rectangle(new Point(Trunc(i) * deltaY, y * deltaX), new Size(deltaY, deltaX)));
                        y += incr;
                        counter++;
                    }
                if (from.X - to.X > 0)
                    for (double i = from.X; i >= to.X; i += delta)
                    {
                        e.FillRectangle(Brushes.IndianRed, new Rectangle(new Point(Trunc(i) * deltaY, y * deltaX), new Size(deltaY, deltaX)));
                        y += incr;
                        counter++;
                    }
                e.FillRectangle(Brushes.IndianRed, new Rectangle(new Point(to.X * deltaY, to.Y * deltaX), new Size(deltaY, deltaX)));
                e.FillRectangle(Brushes.IndianRed, new Rectangle(new Point(from.X * deltaY, from.Y * deltaX), new Size(deltaY, deltaX)));
            }

            stopwatch.Stop();
            time1 = stopwatch.ElapsedTicks;
            Console.WriteLine("Iteration: {0} ticks, {1} iterations", time1.Value, counter);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            n = 40;
            deltaX = this.Height / n;
            deltaY = this.Width / n;
        }

        private void DrawLineBresenhem(Graphics e, Point from, Point to)
        {
            int counter = 0;

            int dx = to.X - from.X;
            int dy = to.Y - from.Y;
            int sx = 0;
            if (dx < 0)
            {
                dx = -dx;
                sx = -1;
            }
            else
            {
                sx = 1;
            }
            
            int sy = 0;
            if (dy < 0)
            {
                dy = -dy;
                sy = -1;
            } else 
            {
                sy = 1;
            }
            bool swap = false;
            if (dx < dy)
            {
                int s = dx; dx = dy; dy = s;
                swap = true;
            }
            int s1 = 2 * dy - dx;
            int x = from.X;
            int y = from.Y;
            e.FillRectangle(Brushes.Brown, new Rectangle(new Point(x * deltaY, y * deltaX), new Size(deltaY, deltaX)));
            int kl = dx;
            stopwatch.Reset();
            stopwatch.Start();
            while (--kl >= 0)
            {
                if (s1 >= 0)
                {
                    if (swap)
                        x += sx;
                    else
                        y += sy;
                    s1 -= 2 * dx;
                }
                if (swap)
                    y += sy;
                else
                    x += sx;
                s1 += 2 * dy;
                e.FillRectangle(Brushes.Brown, new Rectangle(new Point(x * deltaY, y * deltaX), new Size(deltaY, deltaX)));
                counter++;
            }
            time2 = stopwatch.ElapsedTicks;
            Console.WriteLine("Bresenhem: {0} ticks, {1} iterations", time2.Value, counter);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int xs = 0;
            if (!int.TryParse(textBox1.Text, out xs) || xs < 0 || xs > 40)
            {
                textBox1.Text = "0";
                MessageBox.Show("Некорректное значение");
                return;
            }
            from.X = xs;
            from2.X = xs + 5;
            Invalidate();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            int xf = 0;
            if (!int.TryParse(textBox5.Text, out xf) || xf < 0 || xf > 40)
            {
                textBox5.Text = "0";
                MessageBox.Show("Некорректное значение");
                return;
            }
            from.Y = xf;
            from2.Y = xf;
            Invalidate();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int ys = 0;
            if (!int.TryParse(textBox2.Text, out ys) || ys < 0 || ys > 40)
            {
                textBox2.Text = "0";
                MessageBox.Show("Некорректное значение");
                return;
            }
            to.X = ys;
            to2.X = ys + 5;
            Invalidate();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            int yf = 0;
            if (!int.TryParse(textBox3.Text, out yf) || yf < 0 || yf > 40)
            {
                textBox3.Text = "0";
                MessageBox.Show("Некорректное значение");
                return;
            }
            to.Y = yf;
            to2.Y = yf;
            Invalidate();
        }

        public int Trunc(double x)
        {
            int i = (int)x;
            return x - i < 0.5 ? i : i + 1;
        }
    }
}
