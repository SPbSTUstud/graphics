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
        private int n;
        private int deltaX;
        private int deltaY;

        public Form1()
        {
            InitializeComponent();            
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            DrawGrid(e.Graphics);

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
            for (int i = 0; i < n + 1; i++)
                e.DrawLine(new Pen(Brushes.Black, 1), new Point(i * deltaY, 0), new Point(i * deltaY, this.Height));

            for (int i = 0; i < n + 1; i++)
                e.DrawLine(new Pen(Brushes.Black, 1), new Point(0, i * deltaX), new Point(this.Width, i * deltaX));
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            n = 40;
            deltaX = this.Height / n;
            deltaY = this.Width / n;
        }

    }
}
