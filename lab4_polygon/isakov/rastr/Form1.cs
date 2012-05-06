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

        private int[,] canvas;

        private bool fill1 = true;
        private bool fill2; 

        private Point from1 = new Point(1, 1);
        private Point to1 = new Point(5, 10);

        private Point from2 = new Point(6, 1);
        private Point to2 = new Point(10, 10);

        private Point from_;
        private Point to_;

        private Point from2_;
        private Point to2_;


        private Point point = new Point(4, 2);
                       
        long? time1;
        long? time2;

        public Form1()
        {
            InitializeComponent();
            textBox1.Text = from1.X.ToString();
            textBox5.Text = from1.Y.ToString();
            textBox2.Text = to1.X.ToString();
            textBox3.Text = to1.Y.ToString();
            textBox4.Text = from2.X.ToString();
            textBox7.Text = from2.Y.ToString();
            textBox6.Text = to2.X.ToString();
            textBox8.Text = to2.Y.ToString();
            
            canvas = new int[n,n];
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            DrawGrid(e.Graphics);

            canvas = new int[n, n];

            //DrawLine(e.Graphics, from, to);

            DrawLineBresenhem(e.Graphics, from1, to1);
            DrawLineBresenhem(e.Graphics, from2, to2);
            DrawLineBresenhem(e.Graphics, from1, from2);
            DrawLineBresenhem(e.Graphics, to1, to2);

            if (fill1)
                Fill(e.Graphics);
            if (fill2)
            {
                FillRec(e.Graphics, point.X, point.Y);
                e.Graphics.FillRectangle(Brushes.Black, new Rectangle(new Point(point.X * deltaY, point.Y * deltaX), new Size(deltaY, deltaX)));                
            }
            //from_ = new Point(from1.X + 15, from1.Y);
            //from2_ = new Point(from2.X + 15, from2.Y);
            //to_ = new Point(to1.X + 15, to1.Y);
            //to2_ = new Point(to2.X + 15, to2.Y);

            //DrawLineBresenhem(e.Graphics, from_, to_);
            //DrawLineBresenhem(e.Graphics, from2_, to2_);
            //DrawLineBresenhem(e.Graphics, from_, from2_);
            //DrawLineBresenhem(e.Graphics, to_, to2_);
        }

        private void FillRec(Graphics graphics, int x, int y)
        {
            if (canvas[y, x] == 0)
            {
                graphics.FillRectangle(Brushes.Azure, new Rectangle(new Point(x * deltaY, y * deltaX), new Size(deltaY, deltaX)));
                canvas[y, x] = 2;
            }

             if (y - 1 > 0 && canvas[y - 1, x] == 0)
                 FillRec(graphics, x, y - 1);
             if (x - 1 > 0 && canvas[y, x - 1] == 0)
                 FillRec(graphics, x - 1, y);
             if (y + 1 < n && canvas[y + 1, x] == 0)
                 FillRec(graphics, x, y + 1);
             if (x + 1 < n && canvas[y, x + 1] == 0)
                 FillRec(graphics, x + 1, y);
                 
                     
        }

        private void Fill(Graphics e)
        {
            bool draw = false;
            for(int i = 0; i< n; i++)
                for (int j = 0; j < n; j++)
                {
                    //
                    if (draw && canvas[i, j] != 1)
                    {
                        e.FillRectangle(Brushes.IndianRed, new Rectangle(new Point(j * deltaY, i * deltaX), new Size(deltaY, deltaX)));
                        canvas[i,j] = 2;
                    }

                    if (canvas[i, j] == 1 && j + 1 < n && canvas[i, j + 1] == 0)
                          draw = !draw;

                   // if (inpoints(j, i))
                   //     draw = false;
                    
                    int counter = 0;
                    for (int k = j + 1; k < n; k++)
                        if (canvas[i, k] == 1)
                        {
                            counter++;
                            break;
                        }
                    if (counter == 0)
                        draw = false;
                }
        }

        private bool inpoints(int i, int j)
        {
            if (from1.X == i && from1.Y == j)
                return true;
            if (from2.X == i && from2.Y == j)
                return true;
            if (to1.X == i && to1.Y == j)
                return true;
            if (to2.X == i && to2.Y == j)
                return true;
            return false;
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

        private void DrawPolygonIteration(Graphics e, Point from, Point to, double delta)
        {
            int y = from.Y;
            int counter = 0;
            stopwatch.Reset();
            stopwatch.Start();
            for (double i = from.X; i < to.X; i += delta)
            {
                
                e.FillRectangle(Brushes.IndianRed, new Rectangle(new Point(((int)i) * deltaY, (y++) * deltaX), new Size(deltaY, deltaX)));                
            }
            stopwatch.Stop();
            //if (!time1.HasValue)
            //{
                time1 = stopwatch.ElapsedTicks;
                for (double i = from.X; i < to.X; i += delta)
                    counter++;
                Console.WriteLine("Iteration: {0} ticks, {1} iterations", time1.Value, counter);
            //}
            
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
            }
            else
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
            canvas[y, x] = 1;
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
                canvas[y, x] = 1;
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
            from1.X = xs;
            from_.X = xs + 5;
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
            from1.Y = xf;
            from_.Y = xf;
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
            to1.X = ys;
            to_.X = ys + 5;
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
            to1.Y = yf;
            to_.Y = yf;
            Invalidate();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            int xs = 0;
            if (!int.TryParse(textBox4.Text, out xs) || xs < 0 || xs > 40)
            {
                textBox4.Text = "0";
                MessageBox.Show("Некорректное значение");
                return;
            }
            from2.X = xs;
            from2_.X = xs + 5;
            Invalidate();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            int yf = 0;
            if (!int.TryParse(textBox7.Text, out yf) || yf < 0 || yf > 40)
            {
                textBox7.Text = "0";
                MessageBox.Show("Некорректное значение");
                return;
            }
            from2.Y = yf;
            from2_.Y = yf;
            Invalidate();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            int xs = 0;
            if (!int.TryParse(textBox6.Text, out xs) || xs < 0 || xs > 40)
            {
                textBox6.Text = "0";
                MessageBox.Show("Некорректное значение");
                return;
            }
            to2.X = xs;
            to2_.X = xs + 5;
            Invalidate();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            int yf = 0;
            if (!int.TryParse(textBox8.Text, out yf) || yf < 0 || yf > 40)
            {
                textBox8.Text = "0";
                MessageBox.Show("Некорректное значение");
                return;
            }
            to2.Y = yf;
            to2_.Y = yf;
            Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fill1 = false;
            fill2 = true;
            Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fill1 = true;
            fill2 = false;
            Invalidate();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            int yf = 0;
            if (!int.TryParse(textBox9.Text, out yf) || yf < 0 || yf > 40)
            {
                textBox9.Text = "0";
                MessageBox.Show("Некорректное значение");
                return;
            }
            //to2.Y = yf;
            point.Y = yf;
            Invalidate();
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            int yf = 0;
            if (!int.TryParse(textBox10.Text, out yf) || yf < 0 || yf > 40)
            {
                textBox10.Text = "0";
                MessageBox.Show("Некорректное значение");
                return;
            }
            point.X = yf;
            Invalidate();
        }
    }
}
