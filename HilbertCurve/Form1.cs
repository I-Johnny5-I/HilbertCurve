using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HilbertCurve
{
    public partial class Form1 : Form
    {
        private readonly Point[] points;
        private readonly Bitmap bitmap;
        private readonly Graphics graphics;
        private int show;
        private int width;
        private int counter;
        private double h;
        private readonly double s;
        private readonly double v;
        int r, g, b;

        public Form1(int order, int width, int show)
        {
            InitializeComponent();
            this.show = show;
            this.width = width;
            h = 0;
            s = 1;
            v = 1;
            counter = 0;
            int length;
            length = 1024 / (int)Math.Pow(2, order);
            int offset = length / 2;
            int fourthPointOffset = 0;
            if(length == 1 ) fourthPointOffset = 1;
            if (order == 10)
            {
                Width = 1024;
                Height = 1024;
                bitmap = new Bitmap(1024, 1024);
            }
            else bitmap = new Bitmap(1025, 1025);
            graphics = Graphics.FromImage(bitmap);
            pictureBox1.Image = bitmap;
            points = new Point[]
            {
                new Point(offset,offset)
            };
            for (int i = 0; i < order; i++)
            {
                Point[] tempPoints = new Point[(int)Math.Pow(4, order)];
                for (int k = 0; k < 4; k++)
                {
                    for (int j = 0; j < (int)Math.Pow(4, i); j++)
                    {
                        if (k == 0) tempPoints[k * (int)Math.Pow(4, i) + j] = new Point(points[j].Y, points[j].X);
                        if (k == 1) tempPoints[k * (int)Math.Pow(4, i) + j] = new Point(points[j].X, points[j].Y + length);
                        if (k == 2) tempPoints[k * (int)Math.Pow(4, i) + j] = new Point(points[j].X + length, points[j].Y + length);
                        if (k == 3) tempPoints[k * (int)Math.Pow(4, i) + j] = new Point(length * 2 - fourthPointOffset - points[j].Y, length - points[j].X - fourthPointOffset);           
                        
                    }                   
                }

                points = tempPoints;
                length *= 2;
            }
            timer1.Enabled = true;
        }
        
        private void PictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Close();
            Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (show == 0)
            {
                timer1.Interval = 1;
                if (counter == points.Length - 1)
                {
                    counter = 0;
                    //graphics.Clear(Color.Transparent);
                    timer1.Enabled = false;
                }
                h = (double)counter / (points.Length - 1) * 360;
                Color.HsvToRgb(h, s, v, out r, out g, out b);
                graphics.DrawLine(
                    new Pen(System.Drawing.Color.FromArgb(r, g, b),width),
                    points[counter].X, points[counter].Y,
                    points[counter + 1].X, points[counter + 1].Y
                    );
                counter++;
                pictureBox1.Refresh();
            }
            else
            {
                for(int i = 0; i < points.Length - 1; i++)
                {
                    h = (double)i / (points.Length-1) * 360;
                    Color.HsvToRgb(h, s, v, out r, out g, out b);
                    graphics.DrawLine(
                        new Pen(System.Drawing.Color.FromArgb(r, g, b),width),
                        points[i].X, points[i].Y,
                        points[i + 1].X, points[i + 1].Y
                        );
                }
                pictureBox1.Refresh();
                timer1.Enabled = false;
            }
        }
      
    }
}
