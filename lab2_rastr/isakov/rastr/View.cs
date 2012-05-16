using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Life
{
    public partial class Form1 : Form
    {
        private static bool[,] alife = null;
        CancellationTokenSource tokenSource;

        private int n;
        private int deltaX;
        private int deltaY;

        private Life life;

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            DrawGrid(e.Graphics);
            if (alife != null)
            {
                for (int i = 0; i < alife.GetLength(0); i++)
                    for (int j = 0; j < alife.GetLength(1); j++)
                    {
                        if (alife[i,j])
                            e.Graphics.FillRectangle(Brushes.IndianRed, new Rectangle(new Point(i * deltaY, j * deltaX), new Size(deltaY, deltaX)));
                    }
            }
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

            life = new Life(40, 200);

            tokenSource = new CancellationTokenSource();
            CancellationToken ct = tokenSource.Token;            

            // C#
            var t = Task.Factory.StartNew(() =>
                {
                    while(true)
                    {
                        //evaluate life with for subtask
                        life.Evaluate(4);
                        alife = life.GetUniversity().ToBoolArray();                        
                        Invalidate();
                        Thread.Sleep(500);

                        //if cancel
                        if (ct.IsCancellationRequested)
                        {
                            ct.ThrowIfCancellationRequested();
                        }
                    }
                }, tokenSource.Token); //pass cancelation token to task

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
        }
    }
}
