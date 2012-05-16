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
        private float _centerx = 200;
        private float _centery = 150;
        
        public Form1()
        {
            InitializeComponent();
            World3d.Angle = -135;         
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            Draw3dDecart(e.Graphics, _centerx, _centery, 120);

            PointF[] points = new PointF[4];
            points[0] = Draw3dPoint(e.Graphics, 25, 25, 25);
            points[1] = Draw3dPoint(e.Graphics, 55, 55, 55);
            points[2] = Draw3dPoint(e.Graphics, 15, 35, 25);
            points[3] = Draw3dPoint(e.Graphics, 35, 25, 35);

            Pen p = new Pen(Brushes.Red);
            for(int i = 0; i<4; i++)
                for(int j = i; j < 4; j++)
                    e.Graphics.DrawLine(p, points[i], points[j]);
            
            PointF[][] projs = new PointF[4][];
            projs[0] = Draw3dPointProjections(e.Graphics, 25, 25, 25);
            projs[1] = Draw3dPointProjections(e.Graphics, 55, 55, 55);
            projs[2] = Draw3dPointProjections(e.Graphics, 15, 35, 25);
            projs[3] = Draw3dPointProjections(e.Graphics, 35, 25, 35);
            p = new Pen(Brushes.Maroon);
            for (int i = 0; i < 4; i++)
                for (int j = i; j < 4; j++)
                {
                    e.Graphics.DrawLine(p, projs[i][0], projs[j][0]); //xz
                    e.Graphics.DrawLine(p, projs[i][1], projs[j][1]); //yz
                    e.Graphics.DrawLine(p, projs[i][2], projs[j][2]); //xy
                }
            
        }

        private PointF[] Draw3dPointProjections(Graphics g, float x, float y, float z)
        {
            Pen p = new Pen(Brushes.Black);
            PointF[] points = new PointF[3];
            points[0] = new PointF(_centerx + World3d.GetProjectionXZ(x, y, z).X, _centery - World3d.GetProjectionXZ(x, y, z).Y);
            g.DrawRectangle(p, points[0].X, points[0].Y, 2, 2);
            //points[1] = World3d.GetProjectionYZ(x, y, z);
            points[1] = new PointF(_centerx + World3d.GetProjectionYZ(x, y, z).X, _centery - World3d.GetProjectionYZ(x, y, z).Y);
            g.DrawRectangle(p, points[1].X, points[1].Y, 2, 2);
            //points[2] = World3d.GetProjectionXY(x, y, z);
            points[2] = new PointF(_centerx + World3d.GetProjectionXY(x, y, z).X, _centery - World3d.GetProjectionXY(x, y, z).Y);
            g.DrawRectangle(p, points[2].X, points[2].Y, 2, 2);
            return points;
        }

        private PointF Draw3dPoint(Graphics g, float x, float y, float z)
        {
            Pen p = new Pen(Brushes.LightSteelBlue);
            PointF point = World3d.GetPoint(x, y, z);
            g.DrawRectangle(p, _centerx + point.X, _centery - point.Y, 1, 1);
            return new PointF(_centerx + point.X, _centery - point.Y);
        }

        public void Draw3dDecart(Graphics g, float x, float y, int size)
        {
            Pen p = new Pen(Brushes.DeepSkyBlue);
           
            g.DrawLine(p, x, y, x + size, y);
            //for(int i=center.X - size; i<center.X + size; i+=10)
            //    g.DrawLine(p, i, center.Y - 1, i, center.Y + 1);
            g.DrawLine(p, x, y - size, x, y);
            PointF z = CoorHelper.ToDecart(size, World3d.Angle);
            g.DrawLine(p, x, y, x + z.X, y - z.Y);
            //for (int i = center.Y - size; i < center.Y + size; i+=10)
            //    g.DrawLine(p, center.X - 1, i, center.X + 1, i);
        }

        
        public void Draw2dPointInDecart(Graphics g, PointF p)
        {
            //Pen pen = new Pen(Brushes.DarkRed);
            //g.DrawRectangle(pen, _decartCenter.X + p.X - 1, _decartCenter.Y - p.Y - 1, 2f, 2f);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            float angle = 0;
            if (!float.TryParse(textBox2.Text, out angle))
                MessageBox.Show("Некорректное значение угла");
            World3d.Angle = -angle;            
            Invalidate();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //if (!float.TryParse(textBox2.Text, out _angle))
            //    MessageBox.Show("Некорректное значение угла");
            //PointF p = CoorHelper.ToDecart(_radius, _angle);
            //textBox3.Text = p.X.ToString();
            //textBox4.Text = p.Y.ToString();
            //Invalidate();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //if (!float.TryParse(textBox2.Text, out _angle))
            //    MessageBox.Show("Некорректное значение угла");
            //PointF p = CoorHelper.ToDecart(_radius, _angle);
            //textBox3.Text = p.X.ToString();
            //textBox4.Text = p.Y.ToString();
            //Invalidate();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            //if (!float.TryParse(textBox2.Text, out _angle))
            //    MessageBox.Show("Некорректное значение угла");
            //PointF p = CoorHelper.ToDecart(_radius, _angle);
            //textBox3.Text = p.X.ToString();
            //textBox4.Text = p.Y.ToString();
            //Invalidate();
        }
    }
}
